﻿using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using Newtonsoft.Json;
using ModMyFactory.Helpers;

namespace ModMyFactory
{
    [JsonObject(MemberSerialization.OptOut)]
    class Settings
    {
        public static Settings CreateDefault(string fileName)
        {
            var defaultSettings = new Settings(fileName)
            {
                ManagerMode = ManagerMode.PerFactorioVersion,

                FactorioDirectoryOption = DirectoryOption.AppData,
                ModDirectoryOption = DirectoryOption.AppData,
                SavegameDirectoryOption = DirectoryOption.AppData,
                ScenarioDirectoryOption = DirectoryOption.AppData,

                FactorioDirectory = string.Empty,
                ModDirectory = string.Empty,
                SavegameDirectory = string.Empty,
                ScenarioDirectory = string.Empty,

                SelectedLanguage = "en",

                MainWindowInfo = WindowInfo.Empty,
                ModGridLength = new GridLength(1, GridUnitType.Star),
                ModpackGridLength = new GridLength(1, GridUnitType.Star),
                SelectedVersion = string.Empty,

                VersionManagerWindowInfo = WindowInfo.Empty,
                OnlineModsWindowInfo = WindowInfo.Empty,
                ModpackExportWindowInfo = WindowInfo.Empty,

                SaveCredentials = false,
                WarningShown = false,
                ShowExperimentalDownloads = false,

                UpdateSearchOnStartup = true,
                IncludePreReleasesForUpdate = false,

                AlwaysUpdateZipped = true,
                KeepOldModVersions = false,
                KeepOldExtractedModVersions = true,
                KeepOldZippedModVersions = false,
                KeepOldModVersionsWhenNewFactorioVersion = true,
                DownloadIntermediateUpdates = false,

                ShowOptionalDependencies = false,
                ActivateDependencies = true,
                ActivateOptionalDependencies = false,

                LoadSteamVersion = false,
            };
            return defaultSettings;
        }

        public static Settings Load(string fileName, bool createDefault = false)
        {
            var file = new FileInfo(fileName);
            if (!file.Exists && createDefault)
            {
                Settings defaultSettings = CreateDefault(fileName);
                defaultSettings.Save();
                return defaultSettings;
            }

            Settings settings = JsonHelper.Deserialize<Settings>(file);
            settings.file = file;
            return settings;
        }

        FileInfo file;

        public ManagerMode ManagerMode;

        public DirectoryOption FactorioDirectoryOption;

        public DirectoryOption ModDirectoryOption;

        public DirectoryOption SavegameDirectoryOption;

        public DirectoryOption ScenarioDirectoryOption;

        public string FactorioDirectory;

        public string ModDirectory;

        public string SavegameDirectory;

        public string ScenarioDirectory;

        [DefaultValue("en")]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public string SelectedLanguage;

        public WindowInfo MainWindowInfo;

        public GridLength ModGridLength, ModpackGridLength;

        public string SelectedVersion;

        public WindowInfo VersionManagerWindowInfo;

        public WindowInfo OnlineModsWindowInfo;

        public WindowInfo ModpackExportWindowInfo;

        public bool SaveCredentials;

        public bool WarningShown;

        public bool ShowExperimentalDownloads;

        [DefaultValue(true)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool UpdateSearchOnStartup;

        public bool IncludePreReleasesForUpdate;

        [DefaultValue(true)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool AlwaysUpdateZipped;

        public bool KeepOldModVersions;

        [DefaultValue(true)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool KeepOldExtractedModVersions;

        public bool KeepOldZippedModVersions;

        [DefaultValue(true)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool KeepOldModVersionsWhenNewFactorioVersion;

        public bool DownloadIntermediateUpdates;

        public bool ShowOptionalDependencies;

        [DefaultValue(true)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool ActivateDependencies;

        public bool ActivateOptionalDependencies;

        public bool LoadSteamVersion;

        [JsonConstructor]
        private Settings()
        { }

        private Settings(string fileName)
        {
            file = new FileInfo(fileName);
        }

        public void Save()
        {
            JsonHelper.Serialize(this, file);
        }

        public DirectoryInfo GetFactorioDirectory()
        {
            const string directoryName = "Factorio";

            switch (FactorioDirectoryOption)
            {
                case DirectoryOption.AppData:
                    return new DirectoryInfo(Path.Combine(App.Instance.AppDataPath, directoryName));
                case DirectoryOption.ApplicationDirectory:
                    return new DirectoryInfo(Path.Combine(App.Instance.ApplicationDirectoryPath, directoryName));
                case DirectoryOption.Custom:
                    return new DirectoryInfo(FactorioDirectory);
            }

            throw new InvalidOperationException();
        }

        public DirectoryInfo GetModDirectory(Version version = null)
        {
            const string directoryName = "mods";

            switch (ModDirectoryOption)
            {
                case DirectoryOption.AppData:
                    if (version != null)
                        return new DirectoryInfo(Path.Combine(App.Instance.AppDataPath, directoryName, version.ToString(2)));
                    else
                        return new DirectoryInfo(Path.Combine(App.Instance.AppDataPath, directoryName));

                case DirectoryOption.ApplicationDirectory:
                    if (version != null)
                        return new DirectoryInfo(Path.Combine(App.Instance.ApplicationDirectoryPath, directoryName, version.ToString(2)));
                    else
                        return new DirectoryInfo(Path.Combine(App.Instance.ApplicationDirectoryPath, directoryName));

                case DirectoryOption.Custom:
                    if (version != null)
                        return new DirectoryInfo(Path.Combine(ModDirectory, version.ToString(2)));
                    else
                        return new DirectoryInfo(ModDirectory);
            }

            throw new InvalidOperationException();
        }

        public DirectoryInfo GetSavegameDirectory()
        {
            const string directoryName = "saves";

            switch (SavegameDirectoryOption)
            {
                case DirectoryOption.AppData:
                    return new DirectoryInfo(Path.Combine(App.Instance.AppDataPath, directoryName));
                case DirectoryOption.ApplicationDirectory:
                    return new DirectoryInfo(Path.Combine(App.Instance.ApplicationDirectoryPath, directoryName));
                case DirectoryOption.Custom:
                    return new DirectoryInfo(SavegameDirectory);
            }

            throw new InvalidOperationException();
        }

        public DirectoryInfo GetScenarioDirectory()
        {
            const string directoryName = "scenarios";

            switch (ScenarioDirectoryOption)
            {
                case DirectoryOption.AppData:
                    return new DirectoryInfo(Path.Combine(App.Instance.AppDataPath, directoryName));
                case DirectoryOption.ApplicationDirectory:
                    return new DirectoryInfo(Path.Combine(App.Instance.ApplicationDirectoryPath, directoryName));
                case DirectoryOption.Custom:
                    return new DirectoryInfo(ScenarioDirectory);
            }

            throw new InvalidOperationException();
        }
    }
}
