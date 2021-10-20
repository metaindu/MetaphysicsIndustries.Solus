# Release Process

1. Create a `prep-release` branch, or similar
2. In that branch:
   * Check `todo.txt` for anythin important, or anything already completed
   * Bump the version numbers
   * Bump the dependency version in `soluscli.nuspec`
   * Check files for license notices
   * Check any other code formatting or static analysis
   * Update the release notes in the nuspec files
   * Clean, build, and run tests
   * Do a dry run of `release.sh` (both of them), without pushing to nuget
     gallery (Use the `DRY_RUN` envvar)
3. Once everything looks good, open a PR for the branch
4. After the PR is merged, create a release in github
5. `git fetch --tags`
6. Run `release.sh` (both of them) to create the package and upload it