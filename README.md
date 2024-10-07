# ParseCalc
A simple free text parsing calculator. For when the inbuilt calculator is not enough, but a scripting language is too much.

ParseCalc will parse a string containing simple math functions and print out the results. Just pass in a string like so.
```
ParseCalc "2 * 8 * 27"
```

You can also split out the string into lines with the \\n escape character like so.
```
ParseCalc "2 * 8 \n27 - 4"
```

Or if you want to use the Windows Forms interface just pass the ui argument like so. 
```
ParseCalc -ui
```
