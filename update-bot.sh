#!/bin/bash

# kill current process, move current bot to backup. Specifying files somewhat to make sure libee_sqlite3.so remains in the directory
kill $(ps aux | grep '[t]ellah' | awk '{print $2}')
echo "mv ./tellahs-library/* ./tl-backup/"
mv ./tellahs-library/tellah* ./tl-backup/
mv ./tellahs-library/nohup.out ./tl-backup/

# copy uploaded info to standard folder and start process
echo "mv ./tl-upload/* ./tellahs-library/"
mv ./tl-upload/* ./tellahs-library/
chmod +x ./tellahs-library/tellahs-library
cd ./tellahs-library
nohup ./tellahs-library >> nohup.out 2>&1 &

# Give the bot some startup time and write out the current log file contents
sleep 3 
cat nohup.out