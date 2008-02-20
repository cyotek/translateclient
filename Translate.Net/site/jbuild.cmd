call ..\bin\Translate.Net.exe -gen_list

"c:\Program Files\Java\jdk1.6.0_04\bin\javac.exe" -g:none -encoding UTF16 -source 1.3 -target 1.1 servicesdata_en.java 
"c:\Program Files\7-Zip\7z.exe" a -tzip -mx9 servicesdata_en.zip servicesdata_en.class

"c:\Program Files\Java\jdk1.6.0_04\bin\javac.exe" -g:none -encoding UTF16 -source 1.3 -target 1.1 servicesdata_uk.java 
"c:\Program Files\7-Zip\7z.exe" a -tzip -mx9 servicesdata_uk.zip servicesdata_uk.class

del *.class
del *.java