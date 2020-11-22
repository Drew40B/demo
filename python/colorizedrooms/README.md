# Introduction

 This is the result of a programming test

## Instructions to run

1. modify `./src/room.txt` to match your floor plan
2. run `docker-compose run roomcolorizer

## Original Instructions

### Colorized Rooms

A floor plan is an abstract representation of a building floor and defined by the distribution of walls and space.  For this project, floor plans are represented by ASCII characters, with walls being “#” and spaces being “ “ (white space).

A room is defined by a cluster of adjacent spaces surrounded by walls and doors.

A door is defined by a single space that divides two collinear walls.

Here is an example of a floor, with 5 rooms:

    ##########
    #   #    #
    #   #    #
    ## #### ##
    #        #
    #        #
    #  #######
    #  #  #  #
    #        #
    ##########

We want to make the rooms easily distinguishable from each other by using colors.

Write a simple piece of software that receives an ASCII floor plan and prints a nice, beautiful and colorful rendering of the floor plan with the rooms colorized.

### For this project, consider

- All spaces are reachable.
- Edge cases you can ignore:
  - Rooms 1 space wide (for example, narrow hallways).
- Use any programming language you like.
- Use your creativity for color choice and rendering style.
- Spend no more than 1 hour.
- BONUS: feeling bored? Have nothing more interesting to do?
  - Adjacent rooms cannot have the same color. Adjacent rooms are defined by rooms sharing the same wall or door.
- Use the minimum number of colors.
