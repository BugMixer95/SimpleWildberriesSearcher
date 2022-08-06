# SimpleWildberriesSearcher
Test task project for ProductLab.

## About
This is a simple WPF application used to search product cards at [Wildberries](https://www.wildberries.ru/) website and export them to Excel file.

### How to use

1. Choose a file with categories you want to search for (only `.txt` files are allowed).
2. Choose an output folder - exported results would be placed there.
3. Click '*Export to Excel*' button, and just wait - you will be informed when processing is complete (or if anything goes wrong).

![image](https://user-images.githubusercontent.com/54878390/183254243-062c1fe9-fa49-4b1b-9179-faa9110cd1f5.png)

### Remarks
* Output file name is hardcoded to '*SearchOutput.xlsx*'.
* Program is only searching for items at the 1st page and sorting it by popularity.
* Source file must contain categories divided with a line break; otherwise, you could get incorrect result.
