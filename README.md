# Rectangle tiling with cuts

Functionality:
* optimal algorithm - algorithm finding all possible solutions with the least number of cuts (exponential)
* heuristic algorithm - algorithm finding only one solution, not necessarily the best, but in a much shorter time ![](https://latex.codecogs.com/gif.latex?%5Cdpi%7B100%7D%20%5Cbg_white%20%5CTheta%28%284n%29%5E3%29)

The program has two ways of input:
* file - a list of problems to solve is read from **input.txt**
* console - in case of a missing file the program asks to input the parameters in console

File input:
* first line always contains **5** (as the program only solves pentomino puzzles)
* second line tells the program to use the optimal or heuristic algorithm (**hp** or **op**)
* third line gives the program an integer corresponding to a number of pentominoes to be drawn or a list of integers each corresponding to the number of individual pentominoes
* multiple problems might be entered one after another

Example input is given in the file **input.txt**.

The list of pentominoes corresponds to the pentominoes shown below.

<img src="https://github.com/marcelwenka/PentominoSolver/blob/master/img/pentominoes.png" width="80%">

A screenshot of the app is shown below.

<img src="https://github.com/marcelwenka/PentominoSolver/blob/master/img/output.png" width="80%">