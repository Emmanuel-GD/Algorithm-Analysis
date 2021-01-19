---
**Note**
I uploaded this document only for the purpose of helping others. It may be removed anytime 
If the owner (My Instructor) requested to do so.
---
# Algorithm-Analysis - App Created for the purpose of the following assignment

###### Assignment 1

###### SWEG 5012: Advanced Algorithms and Problem Solving
#####Analyzing the Complexity of Algorithms
The assignment aims to develop your insight into the performance of algorithms. You shall look at both the
actual running time of algorithms (in milliseconds of CPU usage) and the complexity of algorithms (in terms
of the number of operations performed). The main emphasis is not so much on coding but is on understanding
the performance of the algorithms and why they perform well and poorly on different inputs. The assignment
deals three sorting algorithms: Insertion-Sort, Quick -Sort and Merge-Sort.
#
# Part 1 . Computing Running Time in Milli Seconds

Perform timing analysis on the three sorting algorithms on the various types of lists(random, ascending,
descending and few items). You should run each sorting algorithm on the various types of lists of increasing
lengths. The actual lengths of the lists for realistic timing in milliseconds depend (of course) on the speed of
your processor. A maximum length of 5,000 or 10,000 may be appropriate, but on slower processors this may
be too large. Plot the count of the number of operations against the length of the lists.
#
# Part 2. Counting the Number of Operations

This task measures the performance of algorithms by counting how many operations are executed during
processing. This is the time complexity of an algorithm. Count the order operation by which we compare
elements. Modify your sorting algorithms to count the number of comparison operations performed. Run the
modified algorithms on the same lists as for the timing analysis and plot the count of the number of operations
against the length of the lists. How does it compare to the timing analysis?
#
# Part 3: Interpretation of Results
Interpret the plots of results in **Part 2**. You will need to explain the shape of the graphs. Looking at 
the timing graphs for random lists, clearly some algorithms grow slower than others as the size of the input
increases. 
#### For example: 
**Are they linear - a straight line, quadratic - grow as n<sup>2</sup>, cubic n<sup>3</sup>, or linear-logarithmic nlog(n), etc.,
where n is the size. Why is this? Also consider the behavior on the special forms of lists. Explain each of these
and compare them to the behavior on the random lists.**

