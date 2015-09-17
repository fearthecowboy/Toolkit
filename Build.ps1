param ( [switch]$Publish, [switch]$Release , [switch]$Debug, [switch]$Clean  )
#ensure that we start in the right folder, and return to where we belong.
pushd -stackname $PSScriptRoot ; try {
    $versionTxtPath = "$PSScriptRoot\version.txt"
    if( -not $Clean ) {
        if( $Debug -and -not $Publish ) {
            write-host -fore yellow "Building Debug version.`n"
            $Configuration = "Debug"
        } else {
            write-host -fore green "Building Release version.`n"
            $Configuration = "Release"
        }
    }    
    
    # find msbuild.exe
    $build = (get-command msbuild.exe -ea 0)

    if( $build ) {
        $build = $build.Source
    } else {
        # not in path. check registry
        $ftc = "HKCU:\Software\FearTheCowboy\Tools"
        $build = ((Get-ItemProperty -Path "$ftc\msbuild.exe" -Name Path -ea 0).Path) 
        
        if( -not $build -or -not (test-path $build )) { 
            $build= (@("${env:ProgramFiles(x86)}","${env:systemroot}\Microsoft.NET") |`
                % {cmd "/c dir /s/b `"$_\msbuild.exe`""  | dir }) |` 
                Select-Object -ExpandProperty VersionInfo |`
                Select-Object FileName, ProductVersion |`
                Sort-Object -Descending { try {[System.Version]$_.ProductVersion } catch { 0 }} | Select-Object -first 1 
            if( $build ) { 
                $build = $build.filename 
                $null = mkdir -Path "$ftc\msbuild.exe" -Force
                $null = New-ItemProperty -Path "$ftc\msbuild.exe" -Name Path -Value $build  -force 
            } 
        } 
    }
    
    if( -not $build ) {
        write-error "Can not find msbuild.exe"
        return;
    }

    if( $Clean ) {
        # switch to the build directory and clean the directories.
        cd $PSScriptRoot
        dir "$PSScriptRoot\*.sln" | %{ 
            & $build $_ "/t:Clean" "/p:Configuration=Release" 
            & $build $_ "/t:Clean" "/p:Configuration=Debug" 
        }
        rmdir -ea 0 -recurse "$PSScriptRoot\output"
        rmdir -ea 0 -recurse "$PSScriptRoot\intermediate"
    } else {
        # use nuget.exe to restore packages
        $nuget = (resolve-path $PSScriptRoot\.solution\nuget.exe -ea 0).Path
        if( $nuget ) {
            dir "$PSScriptRoot\*.sln" | %{ & $nuget restore "$_" }
        } 

        # switch to the build directory and build it.
        cd $PSScriptRoot
        dir "$PSScriptRoot\*.sln" | %{ & $build $_ "/t:Rebuild" "/p:Configuration=$Configuration;PublishPackage=$Publish" }
    }

} finally {popd -stackname $PSScriptRoot  }