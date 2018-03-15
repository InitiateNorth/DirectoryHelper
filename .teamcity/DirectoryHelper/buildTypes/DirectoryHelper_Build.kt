package DirectoryHelper.buildTypes

import jetbrains.buildServer.configs.kotlin.v2017_2.*
import jetbrains.buildServer.configs.kotlin.v2017_2.buildSteps.VisualStudioStep
import jetbrains.buildServer.configs.kotlin.v2017_2.buildSteps.visualStudio
import jetbrains.buildServer.configs.kotlin.v2017_2.triggers.vcs

object DirectoryHelper_Build : BuildType({
    uuid = "59c08dfa-18cd-4b05-8817-d191cd4c688c"
    id = "DirectoryHelper_Build"
    name = "Build"

    vcs {
        root(DirectoryHelper.vcsRoots.DirectoryHelper_HttpsGithubComInitiateNorthDirectoryHelperRefsHeadsMaster)

    }

    steps {
        visualStudio {
            name = "step 1"
            path = "DirectoryHelper.sln"
            version = VisualStudioStep.VisualStudioVersion.vs2017
            runPlatform = VisualStudioStep.Platform.x86
            msBuildVersion = VisualStudioStep.MSBuildVersion.V15_0
            msBuildToolsVersion = VisualStudioStep.MSBuildToolsVersion.V15_0
            configuration = "Release"
        }
    }

    triggers {
        vcs {
        }
    }
})
