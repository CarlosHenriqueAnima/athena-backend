pipeline {
    agent any

    stages {
        stage ('Deploy Container') {
            steps {
                script {
                   sh '/var/infra/deploy/athena-back/deploy.sh'
                }                
            }
        }
    }
}
