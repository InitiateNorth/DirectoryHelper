package DirectoryHelper

import DirectoryHelper.buildTypes.*
import DirectoryHelper.vcsRoots.*
import DirectoryHelper.vcsRoots.DirectoryHelper_HttpsGithubComInitiateNorthDirectoryHelperRefsHeadsMaster
import jetbrains.buildServer.configs.kotlin.v2017_2.*
import jetbrains.buildServer.configs.kotlin.v2017_2.Project
import jetbrains.buildServer.configs.kotlin.v2017_2.projectFeatures.VersionedSettings
import jetbrains.buildServer.configs.kotlin.v2017_2.projectFeatures.versionedSettings

object Project : Project({
    uuid = "63f47b20-bc2e-40de-95fa-ffb4486f4532"
    id = "DirectoryHelper"
    parentId = "_Root"
    name = "DirectoryHelper"

    vcsRoot(DirectoryHelper_HttpsGithubComInitiateNorthDirectoryHelperRefsHeadsMaster)

    buildType(DirectoryHelper_Build)

    features {
        versionedSettings {
            id = "PROJECT_EXT_3"
            mode = VersionedSettings.Mode.ENABLED
            buildSettingsMode = VersionedSettings.BuildSettingsMode.USE_CURRENT_SETTINGS
            rootExtId = DirectoryHelper_HttpsGithubComInitiateNorthDirectoryHelperRefsHeadsMaster.id
            showChanges = false
            settingsFormat = VersionedSettings.Format.KOTLIN
            storeSecureParamsOutsideOfVcs = true
        }
    }
})
