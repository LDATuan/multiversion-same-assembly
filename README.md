# Multiversion same assembly

The solution for when use 1 assembly with multi version in same application.

## Check the public token of assembly

Using **PowerShell** to check public key:

    ([system.reflection.assembly]::loadfile([full string pathDll])).FullName

## Create public key for assembly

In Visual Studio 2022, Tools -> Command Line -> Develop Command Prompt

    sn.exe -k "pathfoler_csproject\[namekey].snk"

## Set public key for assembly

In `Property` of project file (cs.proj), tab **Build -> Strong naming -> Strong name key file**
choose the file `.snk` which generate before.

## Set version for assembly

In `Property` of project file (cs.proj), tab **Package -> General -> Version**

Set **Assembly Version** such as `1.0.0.0`

## How to load multiversion of assembly file in same application

1.  Create xml file which the name is `[assemblyName].dll.config`
2.  Insert xml content

        <?xml version="1.0" encoding="utf-8" ?>
        <configuration>
            <runtime>
                <assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">
                    <dependentAssembly>
                        <assemblyIdentity name="[Name Assembly]" publicKeyToken="[Public token key]" culture="neutral" />
                        <bindingRedirect oldVersion="[old version number]" newVersion="[new version number]" />
                    </dependentAssembly>
                </assemblyBinding>
            </runtime>
        </configuration>

3.  Set **Build Action: Content** and **Copy to Output Directory: Copy if newer**
