# fifa-statalyzer
Tool for scraping statistics from FIFA post-match screenshots

Installation:
Unzip the download and make sure you have .NET Framework installed!
https://www.microsoft.com/net/download/framework

Usage:

Copy all of your screenshots to the \images\ folder.
They should be 16:9 (i.e, 1280x720, 1920x1080, 2560x1440), contain nothing but the game screen and be actual screenshots (please don't take a picture of your TV).
Run Fifa Statalyzer.exe and wait (shouldn't take more than a minute unless you're processing 100+ images at once)
Statistics will be dumped to stats.txt in the root program folder.
These can then be easily imported to Excel, SPSS, or other statistics software for analysis (make sure you use the comma separator and first row as column names).

Scraped Stats:
hGoals
aGoals
hShots
aShots
hOnTarget
aOnTarget
hPossession
aPossession
hTackles
aTackles
hFouls
aFouls
hYellows
aYellows
hReds
aReds
hInjuries
aInjuries
hOffsides
aOffsides
hCorners
aCorners
hShotAccuracy
aShotAccuracy
hPassAccuracy
aPassAccuracy
result (0 = draw, 1 = home win, 2 = away win)

Known issues:

- Currently works with FIFA 18 screenshots only. It's easy to make it work with 17 as well but I figured it's too late for people to want that.
- The images folder doesn't clear itself. Make sure you delete everything in it if you don't want duplicate data the next time you use it.
- The stats don't currently identify the player. Everything is defined by Home and Away as of this moment.

TO-DO:

- Recognize player (will probably do this by scanning Home and Away games in different folders)
- Present basic statistical analysis in-app
- GUI
- Support for other post-match screens (player ratings, etc.)
- Support for squad/division profiles to better keep track of stats
- Support for tags (eg. #WL, #tilted, #bronzeteam)
- General enhancements (let me know what you want!)

Technology and Thanks:

This software uses Tesseract engine for the OCR and the ImageProcessor library for image manipulation. Database work is done with SQLite.

https://github.com/tesseract-ocr/tesseract
http://imageprocessor.org/
https://www.sqlite.org/

I'm a beginner programmer but I'm pretty proud of the work I put in here. Let me know what else you want to see done and I'll do my best! Try not to be mean :)

Contact me at usquandts AT gmail if you want. Or send me a couple of bucks on PayPal :P 