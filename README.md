### PEHeaderReader
PEHeaderReader is a console application designed to read and display the PE file header in the console. The application allows you to view various parts of the PE file header based on selected options. The application was written for the purpose of familiarizing with the structure of PE files.

## Usage
To run PEHeaderReader, execute the following command in the command line:
```
PEHeaderReader --file <path_to_PE_file> --header <header_type>
```

## Parameters
--file or -f - The path to the PE file you want to read and display in the console.

--header or -hr - The type of PE file header you want to display. Possible values include:

ALL (default) - Display all parts of the PE file header.
DOS - Display the DOS header.
FILE - Display the File Header.
NT - Display the Optional Header.
OPT - Display optional header information.
SEC - Display section information.

## Examples
#### Read and display the entire PE file header:

```
PEHeaderReader --file myapp.exe --header ALL
```

#### Display only the DOS header:

```
PEHeaderReader --file myapp.exe --header DOS
```

#### Display section information:

```
PEHeaderReader --file myapp.exe --header SEC
```
