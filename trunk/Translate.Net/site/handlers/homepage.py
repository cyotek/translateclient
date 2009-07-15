import os, sys
from google.appengine.ext import webapp
from google.appengine.ext.webapp.util import run_wsgi_app
import logging
import datetime
from google.appengine.ext import db

class MainPage(webapp.RequestHandler):
  def parse(self, header):
        """
        Return a list of language tags sorted by their "q" values.  For example,
        "en-us,en;q=0.5" should return ``["en-us", "en"]``.  If there is no
        ``Accept-Language`` header present, default to ``[]``.
        copied from http://svn.pythonpaste.org/Paste/trunk/paste/httpheaders.py
        (c) 2005 Ian Bicking and contributors; written for Paste (http://pythonpaste.org)
        Licensed under the MIT license: http://www.opensource.org/licenses/mit-license.php
        (c) 2005 Ian Bicking, Clark C. Evans and contributors
        """
        if header is None:
            return []
        langs = [v for v in header.split(",") if v]
        qs = []
        for lang in langs:
            pieces = lang.split(";")
            lang, params = pieces[0].strip().lower(), pieces[1:]
            q = 1
            for param in params:
                if '=' not in param:
                    # Malformed request; probably a bot, we'll ignore
                    continue
                lvalue, rvalue = param.split("=")
                lvalue = lvalue.strip().lower()
                rvalue = rvalue.strip()
                if lvalue == "q":
                    q = float(rvalue)
            qs.append((lang, q))
        qs.sort(lambda a, b: -cmp(a[1], b[1]))
        return [lang for (lang, q) in qs]
  def get(self):

    found = False  
    if 'Accept-Language' in self.request.headers:
        accepted_langs = self.parse(self.request.headers['Accept-Language'])
        if 'uk' in accepted_langs or 'uk-ua' in accepted_langs :
            self.redirect('/index.uk.html')
            found = True    
        elif 'ru' in accepted_langs or 'ru-ru' in accepted_langs :
            found = True    
            self.redirect('/index.ru.html')    
    if not found:
        self.response.headers['Content-Type'] = 'text/html; charset=utf-8'
        path = os.path.join(os.path.dirname(__file__), 'index.en.html')
        body = open(path, "r").read()
        self.response.out.write(body)
    
application = webapp.WSGIApplication(
                                     [('/', MainPage)],
                                     debug=True)

def main():
  run_wsgi_app(application)

if __name__ == "__main__":
  main()