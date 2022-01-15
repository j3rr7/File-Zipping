# File Zipping

## Requirements
- .NET 6 Framework
- Visual studio 2022

# Introduction

Create a zip of a file including the folder where those file belongs to

example: 

We have a folder such as 

###  Input: 
```
  - Root folder
    - folder 1
      - images.jpg
      - other.exe
    - folder 2
      - whatever.png
      - executable.exe
    - folder 3
```
will create a zip that follows
### Output.zip
```
  - Root folder
    - folder 1
      - images.jpg
    - folder 2
      - whatever.png
    - folder 3
```
only filter a specific file extension with hierarchy folder

