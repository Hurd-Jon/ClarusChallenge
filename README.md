# Clarus DevOps Challenge

## Overview

These 2 tasks should ideally be solved using python / ruby with specific log parsing libraries.
If you have this knowledge combined with good regex skills it should be a piece of cake!

Having said that it has been a few years since i used python or ruby and my regex fu is not perfect :)

I used C# and regex where needed to get the tasks done.
C# is the language I am most familiar with.


## Task One : Apache Log Parsing

The problem with this task was coming up with a set of regex patterns to make sense of the Apache log data.
Its not the friendliest log file I have dealt with.

The biggest issue with this challenge is coping with the different numbers of IP addresses at the start of the file.
Some rows have none, some have two, some have three and some have four

Once the log file is nicely parsed the rest of the challenge was easy enough to handle with LINQ

The Output for the file is in a sample log file in the TaskOne/SampleOutput folder

## Task Two : Parsing a file / Removing comments / Writing out to a csv / Removing Octet padding

Reading the file and writing it out in a new format was fine.

I removed the zeros padding the ip address by converting each octet to a number and then converting the number back to a string.

The output for this challenge is in TaskTwo/Output folder
