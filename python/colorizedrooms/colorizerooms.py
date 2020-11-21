
currentColor = 1
import re

def determineColor(line,index,previousRow):
    global currentColor

    char = line[index]

    if (char == "D" or  not char.isspace()):
        return 0

    if (index > 0 and len(previousRow) >= index):

        if (previousRow[index] > 0):
            return previousRow[index]

        if (line[index -1].isspace()):
            return currentColor

     
        currentColor += 1      

    return currentColor

 

def processFile(fileName):
     
    file1 = open(fileName, 'r') 
    Lines = file1.readlines() 

    previousRow = []
    
    # Strips the newline character 
    for line in Lines: 
        currentRow = []
        line = line.strip()
        lineWithDoors = re.sub("# #","#D#",line)
        for index in range ( len ( line )):
            colorCode = determineColor(lineWithDoors,index,previousRow)
            print(colorCode,end="")
            currentRow.append(colorCode)
          #  print(line[index],end="")
            
        print ("")

        previousRow = currentRow

        #print("Line{}: {}".format(count, line.strip())) 

    print

def main():
   processFile("room.txt")

if __name__ == "__main__":
    main()
