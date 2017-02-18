# IronShell
Small application to override trash programs that try to fuck with your mic (Skype, Plays.TV,... name it)

Yes thats the repo description, because thats exactly what it does.
(Basically it just forces your mic volume to a specific level you can set yourself.)
## Download
[Download](https://www.dropbox.com/s/9y3yrfo9twtjvs5/IronShell_18022017.zip?dl=1)

# How do I set this up?
1. Because you are as lazy as I am, and don't want to do any fancy extra work, just download the finished pre-compiled exe off my dropbox.
You need all the files in the zip. **All of them.**

2. Stuff it into some folder of your choice.
For example: 
 + ```Program Files/IronShell```
 + ```Desktop/IronShell```
 
 Doesnt matter. Just make sure it's somewhere safe.

3. Set it up for autostart

 <kbd>Win</kbd>+<kbd>R</kbd>

 Type in ```shell:startup```

 Make a shortcut of IronShell.exe and paste it in the autostart folder.
 
4. Configure config.txt
 
 If you don't, the program will set it to a default of
 
 **Volume: 85**
 
 **Mic #: 0**
 
 # Config.txt explained
 Right now, you can only force the settings for **1 mic at a time**.
 
 It's not possible to force settings on multiple microphones simultaneously.
 Below are examples how to set it up.
 **Please do not add more than one line, it will not work.**
 
 | Volume     | Seperator | Mic Nr  | Explanation
| ------------- |:-------------:| -----:| -----------------------------|
| 100      | ; | 0 | Sets the volume to 100 on Mic #0 |
| 85      | ; | 1 | Sets the volume to 85 on Mic #1 |
| 0      | ; | 0 | Sets the volume to 0 on Mic #0 |
