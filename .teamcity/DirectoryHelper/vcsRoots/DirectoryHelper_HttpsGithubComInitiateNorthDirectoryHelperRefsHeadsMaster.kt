package DirectoryHelper.vcsRoots

import jetbrains.buildServer.configs.kotlin.v2017_2.*
import jetbrains.buildServer.configs.kotlin.v2017_2.vcs.GitVcsRoot

object DirectoryHelper_HttpsGithubComInitiateNorthDirectoryHelperRefsHeadsMaster : GitVcsRoot({
    uuid = "8bd13082-4245-483b-9f06-1eb9e11a68ce"
    id = "DirectoryHelper_HttpsGithubComInitiateNorthDirectoryHelperRefsHeadsMaster"
    name = "https://github.com/InitiateNorth/DirectoryHelper#refs/heads/master"
    url = "https://github.com/InitiateNorth/DirectoryHelper"
    authMethod = password {
        userName = "InitiateNorth"
        password = "credentialsJSON:2932cdb8-e62d-4862-b77d-61e5558b7209"
    }
})
