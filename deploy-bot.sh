dotnet publish -c release -r linux-x64 --self-contained -p:PublishSingleFile=true
scp ./bin/Release/net8.0/linux-x64/publish/* $TL_USER$TL_ADDRESS:$TL_UPLOAD_PATH
ssh $TL_USER$TL_ADDRESS './update-bot.sh; exit'