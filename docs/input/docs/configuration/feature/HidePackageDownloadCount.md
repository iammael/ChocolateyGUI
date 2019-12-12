---
Order: 100
Title: Hide Package Download Count
Description: Allows control over whether package download count is displayed on remote source views.
---

## Resources

Below is a short video which shows this feature in action:

## Example

This feature can be enabled by running the following command:

```powershell
chocolateyguicli feature enable --name="'HidePackageDownloadCount'"
```

This feaure can be disabled by running the following command:

```powershell
chocolateyguicli feature disable --name="'HidePackageDownloadCount'"
```

## Default Value

The default value for this feature is disabled.