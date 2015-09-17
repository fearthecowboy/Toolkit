#ensure that we start in the right folder, and return to where we belong.
pushd -stackname $PSScriptRoot ; try {
    $versionTxtPath = "$PSScriptRoot\version.txt"

    # check for required files.
    if( -not (test-path $versionTxtPath) ) {
        write-error "Can not find version.txt file [$versionTxtPath]"
        return;
    }

    # find msbuild.exe
    $build = (get-command msbuild.exe -ea silentlycontinue)

    if( -not $build ) {
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

    # Update Version.txt file
    $version = [version](Get-Content -Path $versionTxtPath) 
    $newVersion = New-Object -TypeName System.Version -ArgumentList $version.Major, $version.Minor, $version.Build, ($version.Revision + 1) 
    $newVersion | Set-Content -Path $versionTxtPath

    # set the version in the assemblyinfo files.
    (dir -recurse assemblyinfo.cs).FullName | ?{-not ($_ -match "\\intermediate\\") } |% {
        (get-content $_) -replace "Version\(`"(\d+)\.(\d+)(\.(\d+)\.(\d+)|\.*)`"\)", "Version(`"$newVersion`")" | out-file $_
    }

    # switch to the build directory and build it.
    cd $PSScriptRoot
    & $build "/t:Rebuild" "/p:Configuration=Release;VersionAssembly=$newVersion"

} finally {popd -stackname $PSScriptRoot  }