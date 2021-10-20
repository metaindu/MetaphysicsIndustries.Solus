# Release Process

1. Create a `prep-release` branch, or similar
2. In that branch:
   * Bump the version numbers
   * Check files for license notices
   * Check any other code formatting or static analysis
   * do a dry run of `release.sh` (both of them), without pushing to nuget
     gallery
3. Once everything looks good, open a PR for the branch
4. After the PR is merged, create a release in github
5. `git fetch --tags`
6. Run `release.sh` (both of them) to create the package and upload it
