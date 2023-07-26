pipeline {
    agent any

    stages {
        stage ('Build Image') {
            steps {
                script {
                    dockerapp = docker.build("gabriel7cd/athena-back:${env.BUILD_ID}", '-f ./src/Dockerfile ./') 
                }                
            }
        }

        stage ('Push Image') {
            steps {
                script {
                    docker.withRegistry('https://registry.hub.docker.com', 'dockerhub') {
                        dockerapp.push('latest')
                        dockerapp.push("${env.BUILD_ID}")
                    }
                }
            }
        }

        stage ('Deploy Kubernetes') {
            environment {
                tag_version = "${env.BUILD_ID}"
            }
            steps {
                withKubeConfig([credentialsId: 'kubeconfig']) {
                    sh 'sed -i "s/{{tag}}/$tag_version/g" ./deployment.yaml'
                    sh 'kubectl apply -f ./deployment.yaml'
                    sh 'kubectl rollout restart deployment/athena-back'
                }
            }
        }
    }

    post {
        always {
            script {
                    sh "docker rmi -f gabriel7cd/gabriel7cd/athena-back:${env.BUILD_ID}"
                    sh "docker rmi -f registry.hub.docker.com/gabriel7cd/athena-back:${env.BUILD_ID}"
                    sh "docker rmi -f registry.hub.docker.com/gabriel7cd/athena-back:latest"
            }
        }
    }
}
