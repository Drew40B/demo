
import requests

""" Module to generate a random set of names"""

_nameCache = []

def createName() -> [str]:
    global _nameCache

    # If _nameCache is empty then get a set of 100 names
    if len(_nameCache) == 0:
        url = "https://uinames.com/api/?amount=100"
        response = requests.get(url=url)

        # Check response
        if not response.ok:
            raise ValueError("http response is not ok: {0}".format(response.status_code))

        try:
            _nameCache = response.json()
        except :
            raise ValueError("failed to parse {0}.".format(response.text),response.text)
    
    
    result =_nameCache.pop()

    return [result["name"], result["surname"]]