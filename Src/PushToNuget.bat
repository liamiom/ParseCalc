FOR %%F IN (Cli\bin\nupkg\*.nupkg) DO (
 Set filename=%%F
 goto exitpoint
)
:exitpoint

nuget push "%filename%" -source nuget.org