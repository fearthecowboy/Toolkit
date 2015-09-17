param ( [switch]$publish  )
#ensure that we start in the right folder, and return to where we belong.
pushd -stackname $PSScriptRoot ; try {

    # make sure we are in the right spot.
    cd $PSScriptRoot 

    #remove any existing packages. 
    erase *.nupkg 

    # build everything
    &"$PSScriptRoot\build-release.ps1" 
    
    #: loop thru nuspec files and build the packages
    (dir -recurse *.nuspec) | ?{-not ($_ -match "\\intermediate\\") } |% {
        $csproj = "$($_.DirectoryName)\$($_.BaseName).csproj" 
        if( test-path $csproj ) {
            .\tools\nuget pack $csproj -symbols -properties Configuration=Release
        }
    }
    
    # publish if asked to.
    if( $publish )  {
        dir .\*.nupkg | ?{-not ($_ -match "symbols") } |% { tools\nuget push $_ } 
    }

    # copy to the local repository if we have one.
    if( test-path ..\repo ) {
        move *.nupkg ..\repo
    }

} finally {popd -stackname $PSScriptRoot  }