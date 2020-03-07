import base64
import json
import urllib.request

tweet = r'''Paste hier de minifiede Tweet!'''

tweet = # Parse de Tweet!

if 'media' in tweet['entities']:
    for media in tweet['entities']['media']:
        media_url = # ...

        with urllib.request.urlopen(media_url) as response:
            image = response.read()
            image_base64 = # zoek maar uit hoe je de afbeeldingen kan base64encoden en decoden met utf-8
            media['data'] = image_base64

            print(media)
            print(media["data"])
else:
    print("Does not contain any media object.")
