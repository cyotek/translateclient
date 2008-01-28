<script>
var langcodes=new Array("en", "uk", "default")
var langredirects=new Array("index.en.html", "index.uk.html", "index.en.html")

var languageinfo=navigator.language? navigator.language : navigator.userLanguage
var gotodefault=1

function redirectpage(dest){
if (window.location.replace)
window.location.replace(dest)
else
window.location=dest
}

for (i=0;i<langcodes.length-1;i++){
if (languageinfo.substr(0,2)==langcodes[i]){
redirectpage(langredirects[i])
gotodefault=0
break
}
}

if (gotodefault)
redirectpage(langredirects[langcodes.length-1])


</script>