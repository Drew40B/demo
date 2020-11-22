
#!/usr/bin/env python3

import re
from colors import color

currentColor = 0


def determineColor(line, lineColors, index, previousRow):
    global currentColor

    char = line[index]

    # If Door or whitespace then no background
    if (char == "D" or not char.isspace()):
        return 0

    # Verify not first row or first column
    if (index > 0 and len(previousRow) >= index):

        # Copy the colour from cell above if it is not 0
        if (previousRow[index] > 0):
            return previousRow[index]

        # Copy the colour from cell to the left if it is not 0
        if (lineColors[index - 1] > 0):
            return lineColors[index - 1]

        # We're in a new room so add 1 to the current color. IE top and left are BOTH 0
        currentColor += 1

    return currentColor


def processFile(fileName):

    # Open the file and read in a lines array
    file = open(fileName, 'r')
    Lines = file.readlines()

    previousRow = []

    for line in Lines:
        currentRow = []

        # Trim and add doors
        line = line.strip()
        lineWithDoors = re.sub("# #", "#D#", line)

        # loop over each character and determine the color to render
        for index in range(len(line)):
            colorCode = determineColor(
                lineWithDoors, currentRow, index, previousRow)

            # Prints used to devleop and debug. Normally would not be in production code but left in for demo purposes
            # print(colorCode,end="")
            # print(line[index],end="")

            if (colorCode == 0):
                # Don't print a backround color in case color is not black
                print(line[index], end="")
            else:
                print(color(line[index], bg=colorCode), end="")

            currentRow.append(colorCode)

        print()

        previousRow = currentRow

    print()


def main():
    processFile("room.txt")


if __name__ == "__main__":
    main()
