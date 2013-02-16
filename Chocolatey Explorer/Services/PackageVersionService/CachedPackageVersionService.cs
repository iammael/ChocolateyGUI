﻿using System.Collections.Generic;
using Chocolatey.Explorer.Model;

namespace Chocolatey.Explorer.Services.PackageVersionService
{
    /// <summary>
    /// Adapter for PackageVersionService, but caching all already
    /// downloaded PackageVersions internally.
    /// </summary>
    class CachedPackageVersionService : IPackageVersionService, ICacheable
    {

        public delegate void VersionResult(PackageVersion version);
        public event PackageVersionService.VersionResult VersionChanged;

        private IPackageVersionService packageVersionService;
        private IDictionary<string, PackageVersion> cachedVersions;

        public CachedPackageVersionService()
        {
            packageVersionService = new PackageVersionService();
            packageVersionService.VersionChanged += OnUncachedVersionChanged;
            InvalidateCache();
        }

        public void InvalidateCache()
        {
            cachedVersions = new Dictionary<string, PackageVersion>();
        }

        public void PackageVersion(string packageName)
        {
            PackageVersion cachedPackage;
            cachedVersions.TryGetValue(packageName, out cachedPackage);

            if (cachedPackage == null)
            {
                packageVersionService.PackageVersion(packageName);
            }
            else
            {
                OnVersionChanged(cachedPackage);
            }
        }

        /// <summary>
        /// Called when the PackageVersionService that should be
        /// cached is returning a new PackageVersion.
        /// </summary>
        /// <param name="version"></param>
        private void OnUncachedVersionChanged(PackageVersion version)
        {
            cachedVersions.Add(version.Name, version);
            OnVersionChanged(version);
        }

        private void OnVersionChanged(PackageVersion version)
        {
            var handler = VersionChanged;
            if (handler != null) handler(version);
        }
    }
}
