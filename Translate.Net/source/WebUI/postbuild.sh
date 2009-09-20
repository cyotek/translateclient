#! /bin/sh
echo start
cp -v ../source/WebUI/*.css WebUI
cp -v ../source/WebUI/*.asax WebUI
cp -v ../source/WebUI/*.aspx WebUI
cp -v ../source/WebUI/*.js WebUI
cp -v ../source/WebUI/*.png WebUI
cp -v Translate.WebUI* WebUI/bin
echo end
exit 0
