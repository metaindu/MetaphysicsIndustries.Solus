#!/bin/bash

# MetaphysicsIndustries.Solus
# Copyright (C) 2006-2022 Metaphysics Industries, Inc., Richard Sartor
#
# This library is free software; you can redistribute it and/or
# modify it under the terms of the GNU Lesser General Public
# License as published by the Free Software Foundation; either
# version 3 of the License, or (at your option) any later version.
#
# This library is distributed in the hope that it will be useful,
# but WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
# Lesser General Public License for more details.
#
# You should have received a copy of the GNU Lesser General Public
# License along with this library; if not, write to the Free Software
# Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301
# USA

PROJECT_BASE=solus
PACKAGE_BASE=soluscli

TAG="$1"

if [ "$TAG" = "" ]
then
    TAG=$TRAVIS_TAG
fi

if [ "$TAG" = "" ]
then
    echo 'No tag given. Package will not be created.'
    exit 1
fi

VERSION=`echo "$TAG" | perl -ne 'print /^v(\d+\.\d+(?:\.\d+(?:\.\d+)?)?)$/'`
if [ "$VERSION" = "" ];
then
    echo 'Wrong version format. Package will not be created.'
    exit 1
fi

AVERSION=`grep AssemblyVersion Properties/AssemblyInfo.cs | perl -npe 's/^.*?\"//;s/\".*$//'`
if [ "$VERSION" != "$AVERSION" ]
then
    echo "Tag doesn't match assembly version ($VERSION != $AVERSION). Package will not be created."
    exit 1
fi

DVERSION=`echo "$VERSION" | perl -ne 'print /^(\d+\.\d+\.\d+(?:\.\d+)?)/'`
if [ "$DVERSION" = "" ];
then
    DVERSION="${VERSION}.0"
    echo "Adjusting version to '$DVERSION'."
else
    DVERSION="$VERSION"
fi

echo 'Creating the nuget package...'
if ! dotnet pack --include-source --include-symbols -o ./ $PROJECT_BASE.csproj /p:PackageVersion=$VERSION ; then
    echo 'Error creating the package. The package will not be uploaded.'
    exit 1
fi

if [ -z "$NUGET_API_KEY" ]; then
    echo 'No Api Key provided. The package will not be uploaded. Please set the $NUGET_API_KEY variable.'
    exit 1
fi

if [ -n "$DRY_RUN" ]; then
    echo 'This is a dry run. The package will not be uploaded.'
else
    echo 'Uploading the package to nuget...'
    if ! nuget push $PACKAGE_BASE.$DVERSION.nupkg -Source nuget.org -ApiKey $NUGET_API_KEY ; then
        echo 'Error uploading the package. Quitting.'
        exit 1
    fi
fi

echo 'Done.'
