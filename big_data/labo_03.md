# Labo Elastic Stack

## Labo inhoud

**Laat je niet afschrikken!** Dit labo bevat best veel leesvoer. Het m

### Opgave

Je gaat aan de slag met de Elastic Stack. Je zal een Elasticsearch cluster opzetten, Logstash en Kibana. De focus ligt niet zozeer op het opzetten van deze pipeline, wel op het verwerken en analyseren van data. 

### Deadline

Ten laatste voor de start van het volgende labo.
### Indienen

Je dient het verslag in via Leho – Opdrachten (als PDF-bestand).

## Inleiding

In de theorielessen hebben jullie kennis gemaakt met data intensieve applicaties en hun specifieke vereisten. Jullie zijn aan de slag gegaan met een klassieke RDBMS oplossing (MariaDB). In dit labo gaan we bekijken hoe je een gedistribueerde NoSQL oplossing kan uitrollen. Om de werking hiervan te demonstreren zullen we data afkomstig van Twitter gebruiken. De Tweets zijn geslecteerd op *#metoo*. Jullie gaan een aantal analyses uitvoeren en deze alsook visualiseren.

Hiervoor gaan jullie aan de slag met de zogenaamde [Elastic Stack](https://www.elastic.co/products/). De dataset bestaat uit Tweets die geselecteerd zijn op de hashtag *#metoo*. De bedoeling is om m.b.v. [Logstash](https://www.elastic.co/products/logstash), [Elasticsearch](https://www.elastic.co/products/elasticsearch) en [Kibana](https://www.elastic.co/products/kibana) de data te **verwerken**, te **analyseren** en te **visualiseren**. Deze combinatie van software werd vroeger de [ELK Stack](https://www.elastic.co/elk-stack) genoemd. 

### Doestelling

De bedoeling van dit labo is **niet** om na te gaan hoe snel je kan lezen, commando’s kan invoeren en de output kan plakken in een document.

De bedoeling van dit labo is **wel** dat je de tijd neemt om rustig op verkenning te gaan en bij te leren over het systeem waarmee we werken. De opgave dient enkel als leidraad voor jouw zoektocht. Maak er dus geen race tegen de klok van.



| Kennen                     | Kunnen |
| -------------------------- | ------ |
| Compontenten Elastic Stack | Elasticsearch cluster opzetten m.b.v. Docker containers   |
| Elastic terminologie       | Basis handelingen in Kibana: queries uitvoeren en visualisatie   |
| Elasticsearch architectuur | Basis queries Elasticsearch   |



### Benodigdheden

* Je eigen laptop
* Je krijgt toegang tot een VM met:
    * Ubuntu 18.04.4 (4.15.0-88-generic)
    * Docker 19.03.6
    * docker-compose 1.25.4
    * De map `~/labo` met daarin de nodige code
    * De credentials zijn teurg te vinden op Leho

### De pipeline

In dit labo krijgen jullie toegang tot een endpoint waarop de data reeds beschikbaar is. Die data komt natuurlijk niet uit het niets. Aan het einde van dit semester zouden jullie de volledige architectuur moeten begrijpen (en kunnen uitleggen).

![](https://i.imgur.com/MlKvoGl.jpg)

Twitter heeft de mogelijkheid om data op te vragen a.d.h.v. een [API](https://developer.twitter.com/). Hiervoor is een developer account vereist. In dit labo wordt deze API aangesproken via een Python library, namelijk [Tweepy](http://docs.tweepy.org/en/latest/). Met een paar lijntjes code worden alle Tweets met *#metoo* opgevraagd en opgeslagen (in JSON-formaat).

Tijdens het labo zal een ander Python script de data uitlezen. Het script gaat niets met de data doen, buiten het aan een bepaalde snelheid "producen". Hierdoor ontstaat er als het ware een constante stream aan data.

De Tweets worden (tijdelijk) opgeslagen (producen) in een zogenaamde message broker ([Kafka](https://kafka.apache.org/)). Beschouw Kafka gerust als een centrale opslag waarop andere applicaties kunnen inpikken. 

Deze stream zal worden uitgelezen (consumen) door Logstash, behorende tot de Elastic Stack. Logstash kan beschouwd worden als een ETL-tool (Extraction, Transformation, Load). Elke Tweet wordt bekeken, overtollige JSON fields worden weggegooid, er wordt een veld toegevoegd, ... . Uiteindelijk zal Logstash de data doorpompen naar Elasticsearch. 

Tot slot gaan we nog een andere component uit de Elastic Stack gebruiken, namelijk Kibana. Deze tool laat o.a. toe om de data op een gebruiksvriendelijke manier te ondervragen evenals te visualiseren. Daarnaast kan Kibana gebruikt worden om de Elastic Stack te managen. 

In principe zou deze case perfect uitgewerkt kunnen worden met uitsluitend Logstash ([Twitter input plugin](https://www.elastic.co/guide/en/logstash/7.6/plugins-inputs-twitter.html)), Elasticsearch en Kibana. Zonder Kafka dus. De Twitter API heeft bepaalde limitaties, dit is dan ook de voornaamste reden waarom voor deze oplossing is gekozen.

Logstash heeft zogenaamde [input plugins](https://www.elastic.co/guide/en/logstash/7.6/input-plugins.html), o.a. eentje voor [Twitter](https://www.elastic.co/guide/en/logstash/7.1/plugins-inputs-twitter.html). Op die manier is het mogelijk om de data rechtstreeks via de Twitter API op te vragen. Daarnaast heeft Logstash gelijkaardige mechanismen zoals Kafka aan boord, weliswaar gelimiteerd. Wie daar interesse in heeft, kan best eens kijken naar [Logstash persistent queues](https://www.elastic.co/guide/en/logstash/7.6/persistent-queues.html).

Leuk als je dit alles hebt gelezen (en hopelijk begrepen). De voorbije jaren werd de data wel degelijk beschikbaar gesteld via een Kafka cluster. Dit jaar zijn jullie met veel studenten, om problemen te vermijden heb ik een alternatief uitgewerk. Het Kafka gedeelte wordt gesimuleerd door een Python script. Dit script is een socket server en zal de data als het ware streamen. 

### Elasticsearch achtergrondinfo

Elasticsearch is een NoSQL database. De definitie hiervan is jammer genoeg niet volledig eenduidig. Met NoSQL wordt soms verwezen naar "non SQL" maar even goed naar "not only sql". In principe kunnen we stellen dat het een "next gen database" is, een systeem dat gebouwd is met schaalbaarheid, fouttolerantie en big data in het achterhoofd.

De website db-engines.com geeft een zicht hoe populair een bepaald systeem is. Elasticsearch categoriseren ze op hun website onder de noemer "search engines". Elasticsearch is tevens de meest gebruikte oplossing in die categorie. Onder een "search engine" verstaat men dat je je data kan ondervragen aan de hand van complexe "search expressions". Denk bijvoorbeeld aan een nieuwsartikel, hierin kunnen we gaan zoeken op woorden, (volledige) zinnen, veelvoorkomende termen, ... . Er zijn eveneens operaties mogelijk om geospatiale data te ondervragen. Men werkt met een scoring (ranking) systeem: hoe ver of hoe dicht ligt het resultaat bij wat je complexe query vraagt.

Als we de documentatie van Elasticsearch erop nalezen kunnen we volgende zaken concluderen:
* Document oriented database
* Geen transacties
* Gebouwd voor snelheid
* Ondersteunt time series

Wat dit precies allemaal wil zeggen werd reeds behandeld in de inleidende presentatie. Doorloop deze eventueel nog eens even kort, zo dat je goed weet wat Elasticsearch precies is. 

## Elastic Stack opzetten

De Elastic Stack ondersteunt zowat alle populaire operating systems, [installatie](https://www.elastic.co/guide/en/elastic-stack/7.6/installing-elastic-stack.html) kan via een archief, package en eventueel met kant-en-klare Docker containers. In dit labo is voor de laatste optie gekozen. 

Het correct opzetten en configureren van de Elastic Stack kan tijdrovend zijn, vooral omwille van gevoelige config files. Vraag maar aan studenten van de vorige jaren ;-). Wil je meer info (voor project, bachelorproef, ...)?  Vraag gerust!

### docker-compose.yml

Navigeer naar de `labo` directory, en bekijk de inhoud van `docker-compose.yml`. Neem je tijd om dit bestand goed te bestuderen. Graag ook de environment variables verklaren.

> **Antwoord**  
> Deze defineren informatie over een elastic node en tot welke cluster deze behoren.

### kafka_logstash.conf

Het bestand `kafka_logstash.conf` beschrijft een zogenaamde [Logstash pipeline](https://www.elastic.co/guide/en/logstash/7.6/pipeline.html).

De eerste blok configureert de [TCP input plugin](https://www.elastic.co/guide/en/logstash/7.6/plugins-inputs-tcp.html). De `codec` geeft aan in welk formaat de data mag verwacht worden, in dit geval zijn de Tweets opgeslagen in een JSON-formaat. Elasticsearch zelf gebruikt eveneens dit formaat.

Men kan gebruik maken van meerdere inputs, eventueel kan de data gecombineerd worden of verder verrijkt worden d.m.v. [lookup enrichments](https://www.elastic.co/guide/en/logstash/7.6/lookup-enrichment.html).

```
input {
  tcp {
    host => "socketserver"
    port => 10000
    codec => "json_lines"
    mode => "client"
  }
}
```

#### (Optioneel) Ter info - Kafka input plugin

Voor degene die graag eens willen experimenten met Kafka geef ik hier wat extra info mee. In dit labo is het wellicht nog iets te vroeg, maar eens we Kafka hebben gezien kan je nog is terugdenken aan dit labo (en onderstaande uitproberen). 

De [TCP input plugin](https://www.elastic.co/guide/en/logstash/7.6/plugins-inputs-tcp.html) zou kunnen vervangen worden door de [Kafka input plugin](https://www.elastic.co/guide/en/logstash/7.6/plugins-inputs-kafka.html). Dit zijn m.u.v. `codec` allemaal standaard [Kafka consumer settings](https://kafka.apache.org/documentation/#consumerconfigs).

Merk op dat `${IP}` verwijst naar een environment variable (reeds ingesteld op je VM). Doe gerust eens `echo $IP`. De `${VAR}` notatie is de syntaxis die je kan gebruiken i.c.m. docker-compose. 

```
input {
  kafka {
    bootstrap_servers => "bd-kafka-00.cloud2.local:9092"
    auto_offset_reset => "earliest"
    group_id => "${IP}"
    topics => ["metoo-workshop-test"]
    codec => json
  }
}
```
<small>Einde info "Kafka input plugin"</small>
---

#### Data transformatie

In de tweede blok wordt de data getransformeerd. Het Python script dat de Twitter API bevraagt kuist de data niet op, m.a.w. elk mogelijk veld dat bij een Tweet hoort wordt doorgestuurd naar de Kafka cluster. 

Veel van die velden hebben we niet nodig, deze gaan we dan ook niet opslaan in de Elasticsearch cluster. 

Het veldje `source` wordt hernoemd, aangezien dit in de context van Elasticsearch verwarrend kan zijn. Wanneer in Elasticsearch een document wordt geïndexeerd (toegevoegd) wordt de originele content altijd bijgehouden in [`_source`](https://www.elastic.co/guide/en/elasticsearch/reference/7.6/mapping-source-field.html). Een logischere naam is `device`: het type apparaat of platform vanwaar de Tweet is verzonden (bv. Twitter for Android).

Bij `device` wordt normaal gezien altijd een URL meegegeven, dus bv. een link om een of andere app te downloaden. Met `gsub` halen we deze overbodige data weg. 

Met de `date` filter zorgen we dat de datum correct geparsed en opgeslagen wordt als een timestamp. 

Tot slot wordt er tijdens het *indexeren* een stukje Ruby code uitgevoerd. Het aantal karakters van elke Tweet wordt opgeslagen in het veld `tweet_lenght`. In principe kan dit ook bepaald worden wanneer we de data bevragen. Dit impliceert dat de lengte telkens opnieuw zou berekend worden, wat niet bijster efficiënt zou zijn. 

```
filter {
  mutate {
    remove_field => ["id_str", "place", "..."]
  }
  mutate {
    rename => ["source", "device" ]
  }
  mutate {
    gsub => [
      "device", "<.*?>", ""
    ]
  }
  date {
    match => [ "created_at", "EEE MMM dd HH:mm:ss Z yyyy" ]
    locale => "en-US"
  }
  ruby {
    code => 'event.set("tweet_length", event.get("full_text").length)'
  }
}
```

#### Data output

Deze laatste blok beschrijft de [output plugins](https://www.elastic.co/guide/en/logstash/7.6/output-plugins.html). Telkens een Tweet verwerkt wordt, zal een *"."* naar de console geprint worden. Daarnaast zal de opgekuiste data opgeslagen worden in de Elasticsearch cluster. Het gedeelte i.v.m. `templates` komt later in dit labo aan bod.

Net zoals bij de *inputs* kunnen er meerdere *outputs* worden ingesteld. Logstash kan m.a.w. volledig los gebruikt worden van Elasticsearch. 

```
output {
  stdout { codec => dots }
  # stdout { codec => rubydebug }
  elasticsearch {
      hosts => ["es01:9200", "es02:9200", "es03:9200"]
      index => "tweets"
      template => "/labo/twitter_template.json"
      template_name => "tweets"
      template_overwrite => true
  }
}
```

### Stack starten

Start de stack (de services gedefinieerd in `docker-compose.yml`).

```
cd ~/labo
docker-compose up -d
```

Waarvoor dient de `-d`?
> **Antwoord**   
> DIt zorgt ervoor date de container in de cahtergrond worden gestart.

Na enkele seconden zou de output er als volgt moeten uitzien:
![](https://i.imgur.com/hZQlDjj.png)

#### Status van de services (containers) controleren

Controleer de status van de containers. De output zou er (ongeveer) als volgt moeten uitzien:

```
   Name                 Command               State                Ports
--------------------------------------------------------------------------------------
es-node-01   /usr/local/bin/docker-entr ...   Up      0.0.0.0:9200->9200/tcp, 9300/tcp
es-node-02   /usr/local/bin/docker-entr ...   Up      9200/tcp, 9300/tcp
es-node-03   /usr/local/bin/docker-entr ...   Up      9200/tcp, 9300/tcp
kibana       /usr/local/bin/kibana-docker     Up      0.0.0.0:5601->5601/tcp
logstash     /usr/local/bin/docker-entr ...   Up      5044/tcp, 9600/tcp
```

Hoe heb je de status nagekeken?

> `docker-compose ps`

Bekijk ook eens de logs van de verschillende services. Hoe doe je dit? Zoek / probeer zeker ook eens uit wat de `-f` parameter doet.

> `docker-compose logs`  
> `-f` parameter zorgt ervoor dat je constant logs blijft krijgen


#### Status van de Elasticsearch cluster controleren

De documentatie van Elasticsearch, en bij uitbreiding die van de Elastic Stack is zeer goed. Controleer de [cluster health](https://www.elastic.co/guide/en/elasticsearch/reference/7.6/cluster-health.html). 

De output zou er als volgt moeten uitzien:

```
{
  "cluster_name" : "docker-cluster",
  "status" : "green",
  "timed_out" : false,
  "number_of_nodes" : 3,
  "number_of_data_nodes" : 3,
  "active_primary_shards" : 4,
  "active_shards" : 11,
  "relocating_shards" : 0,
  "initializing_shards" : 0,
  "unassigned_shards" : 0,
  "delayed_unassigned_shards" : 0,
  "number_of_pending_tasks" : 0,
  "number_of_in_flight_fetch" : 0,
  "task_max_waiting_in_queue_millis" : 0,
  "active_shards_percent_as_number" : 100.0
}
```

Hoe heb je de status nagekeken? 

> 1. via kibana - dev console
> 2. via terminal `curl localhost:9200/_cluster/health`
> 3. via browser `http://172.23.82.60:3032/_cluster/health`

Wellicht d.m.v. een commando. Probeer er ook eens naar te surfen. Maak een screenshot (indien je met hackmd.io werkt kan je gewoon "plakken"; wordt automatisch geupload naar [imgur](https://imgur.com/); een URL is ook prima): 

> Antwoord
> ...

Probeer tot slot ook nog eens volgende commando's

```
curl -XGET http://localhost:9200
curl -XGET http://localhost:9200/_cat/nodes?v
curl -XGET http://localhost:9200/_nodes?pretty | less
curl -XGET http://localhost:9200/_nodes/stats?pretty | less
```

Wat is de output? Wat geeft het weer?

> Antwoord  
> 1. geeft info over de cluster
> 2. geeft de status van de verschillende nodes -> `_cat` geeft een beknopte weergave om snel te kijken
> 3. geeft alle algemene informatie van de verschillende nodes terug
> 4. geeft stats terug van de verschillende nodes. eg index, shards

## Data bevragen

Als alles goed is, zouden er nu voortdurend nieuwe Tweets in je Elasticsearch cluster moeten terechtkomen. De data bevragen, analyseren en visualiseren gaan jullie doen m.b.v. Kibana.

### Kibana

Normaal gezien zou je moeten kunnen surfen naar http://172.23.82.60:kibana-poort. Klik op "Explore on my own". Vooraleer effectief de data te bevragen, kan je indien gewenst monitoring inschakelen. Dit geeft een snel, mooi overzicht van je Elastic Stack.

![](https://i.imgur.com/sVDzOit.png)

Na een dertigtal seconden zou de interface gelijkaardig moeten zijn aan onderstaande screenshot. Merk op: er kan eventueel gespeeld worden met de time-range & auto-refresh. 

![](https://i.imgur.com/NEY13uH.png)

Daarna mag je navigeren naar de *"Dev Tools"* (steeksleutel). 

### Data verkennen

Voer volgende query uit. Doe dit door op de groene pijl te klikken of door de toetsencombinatie `CTRL + ENTER`. De query's worden *"in blok"* uitgevoerd, je kan dus meerdere query's onder mekaar plaatsen. 

```
GET tweets/_search
```

De output ziet er wellicht ongeveer zo uit:

![](https://i.imgur.com/f5mZRsR.png)

Alvast een aantal opmerkingen:

* Took: hoelang duurde het om de query uit te voeren
    * Indien de query een aantal keer uitgevoerd wordt, zal dit verlagen (caching)
* Het totaal aantal hits is groter dan of gelijk aan (gte) 10000.
    * Sinds [Elasticsearch versie 7.0](https://www.elastic.co/blog/elasticsearch-7-0-0-released) wordt soms een schatting gemaakt, dit is sneller. 
    * Om het exact aantal te krijgen kan volgende query gebruikt worden: 
      ```
      GET tweets/_search
      {
        "track_total_hits": true
      }
      ```
* De `score` en `max_score` zal in dit geval altijd 1 zijn: hoe goed is het antwoord op de query.


Hoeveel Tweets zijn er in totaal?

> **Antwoord**  
> 23000+

Een Tweet bestaat uit meerdere `key:value` paren. De key `full_text` bevat de feitelijke inhoud van een Tweet. Volgende query zoekt Tweets waarin gesproken wordt over "harvey WEINSTEIN". Bekijk de resultaten, valt er iets op?

```
GET tweets/_search
{
  "query": {
    "match": {
      "full_text": "harvey WEINSTEIN"
    }
  }
}
```

> **Antwoord**  
> De queries zijn niet hoofdletter gevoelig

Pas de vorige query aan, zoek naar "Bart De Pauw". Zijn de resultaten bruikbaar? Focus op de niet Nederlandstalige Tweets (dat zou ook met een query opgelost kunnen worden). 

> **Antwoord**  
> De resultaten bevatten niet enkel tweets met "Bart De Pauw" in zijn geheel, maar ook met "Bart" of "De". 

De match query behandeld elk woord apart, tussen elk woord kan het als het ware een "`OR`" gezet worden. Om de vorige query te verbeteren kan er ook een "`AND`" tussen elk woord gezet worden. Bekijk de documentatie van de [match query](https://www.elastic.co/guide/en/elasticsearch/reference/7.6/query-dsl-match-query.html) en pas de vorige query aan.

> **Antwoord**
```
GET tweets/_search
{
  "query": {
    "bool": {
      "must": [
        {
          "match": {
            "full_text": {
              "query": "Bart de pauw",
              "operator": "and"
            }
          }
        }
      ],
      "must_not": [
        {
          "term": {
            "lang": {
              "value": "nl"
            }
          }
        }
      ]
    }
  }
}
```

Een andere oplossing zou volgende query kunnen zijn, gebruik makend van [minimum should match](https://www.elastic.co/guide/en/elasticsearch/reference/7.6/query-dsl-minimum-should-match.html):

```
GET tweets/_search
{
  "query": {
    "match": {
      "full_text": {
        "query": "bart de pauw",
        "minimum_should_match": 2
      }
    }
  }
}
```

Er zijn nog tal van andere manieren om een accuraat antwoord te krijgen op vragen zoals bovenstaande. Bekijk bijvoorbeeld eens de [match phrase query](https://www.elastic.co/guide/en/elasticsearch/reference/6.6/query-dsl-match-query-phrase.html) of de [bool query](https://www.elastic.co/guide/en/elasticsearch/reference/current/query-dsl-bool-query.html). 

Verschillende type queries kunnen eenvoudig gecombineerd worden, bekijk volgende query eens aandachtig:

```
GET tweets/_search
{
  "query": {
    "bool": {
      "must": [
        {
          "match": {
            "full_text": "weinstein"
          }
        },
        {
          "range" : {
            "tweet_length": {
                "gte" : 100
            }
          } 
        }
      ],
      "must_not": [
        {
          "match": {
            "lang": "nl"
          }
        }
      ]
    }
  }
}
```

Wat doet deze query?

> **Antwoord**  
> Hij zoekt naar alle tweets waarin "weinstein" voorkomt en die een minimum lengte van 100 karakters en die niet in het nederelands zijn.



### Eenvoudige analyses

Volgende query haalt alle Nederlandstalige Tweets op:

```
GET tweets/_search
{
  "query": {
    "match": {
      "lang": "nl"
    }
  }
}
```

Volgende query is een zogenaamde [aggregation](https://www.elastic.co/guide/en/elasticsearch/reference/current/search-aggregations.html) meer bepaald een [metrics aggregation](https://www.elastic.co/guide/en/elasticsearch/reference/current/search-aggregations-metrics.html). Wat is de gemiddelde lengte van een Tweet? Merk op de `"size": 0` wordt meegegeven, de Tweets zelf zullen dus niet worden weergegeven.

```
GET tweets/_search
{
  "size": 0, 
  "aggs": {
    "average_message_size": {
      "avg": {
        "field": "tweet_length"
      }
    }
  }
}
```

Combineer vorige queries om de gemiddelde lengte van alle Nederlandstalige Tweets te bepalen. 

> **Antwoord**  
> 145 karakters

Hoeveel karakters telt de langste Tweet, ongeacht de taal. (tip: metric aggregations)?

> **Antwoord**  
> 947 karakters

Hoeveel karakters telt de kortste Tweet?

> **Antwoord** 
> 11 karakters

Volgende query biedt een antwoord op de twee vorige queries:

```
GET tweets/_search
{
  "size": 0,
  "aggs": {
    "tweet_length_stats": {
      "stats": {
        "field": "tweet_length"
      }
    }
  }
}
```

Elasticsearch laat ons ook toe om [histograms](https://www.elastic.co/guide/en/elasticsearch/reference/current/search-aggregations-bucket-histogram-aggregation.html) te genereren. Volgende query deelt de Tweets in op basis van lengte. 

```
GET tweets/_search
{
  "size": 0,
  "aggs": {
    "tweet_length_histogram": {
      "histogram": {
        "field": "tweet_length",
        "interval": 50
      }
    }
  }
}
```

Nest een 2de aggregatie, namelijk eentje die de stats binnen elke bucket weergeeft (zie eerder dit document). Het resultaat ziet er dan bijvoorbeeld als volgt uit:

```
{
  "took" : 49,
  "timed_out" : false,
  "_shards" : {
    "total" : 5,
    "successful" : 5,
    "skipped" : 0,
    "failed" : 0
  },
  "hits" : {
    "total" : 16929,
    "max_score" : 0.0,
    "hits" : [ ]
  },
  "aggregations" : {
    "tweet_length_histogram" : {
      "buckets" : [
        {
          "key" : 0.0,
          "doc_count" : 949,
          "tweet_stats_bucket" : {
            "count" : 949,
            "min" : 6.0,
            "max" : 49.0,
            "avg" : 30.331928345626977,
            "sum" : 28785.0
          }
        },
        {
          "key" : 50.0,
          "doc_count" : 1505,
          "tweet_stats_bucket" : {
            "count" : 1505,
            "min" : 50.0,
            "max" : 99.0,
            "avg" : 77.93887043189369,
            "sum" : 117298.0
          }
        },
        {
          "key" : 100.0,
          "doc_count" : 11352,
          "tweet_stats_bucket" : {
            "count" : 11352,
            "min" : 100.0,
            "max" : 149.0,
            "avg" : 137.22489429175477,
            "sum" : 1557777.0
          }
        },
        {
          "key" : 150.0,
          "doc_count" : 914,
          "tweet_stats_bucket" : {
            "count" : 914,
            "min" : 150.0,
            "max" : 199.0,
            "avg" : 171.84463894967178,
            "sum" : 157066.0
          }
        },
        {
          "key" : 200.0,
          "doc_count" : 756,
          "tweet_stats_bucket" : {
            "count" : 756,
            "min" : 200.0,
            "max" : 249.0,
            "avg" : 224.30291005291005,
            "sum" : 169573.0
          }
        },
        {
          "key" : 250.0,
          "doc_count" : 1099,
          "tweet_stats_bucket" : {
            "count" : 1099,
            "min" : 250.0,
            "max" : 299.0,
            "avg" : 275.21019108280257,
            "sum" : 302456.0
          }
        },
        {
          "key" : 300.0,
          "doc_count" : 316,
          "tweet_stats_bucket" : {
            "count" : 316,
            "min" : 300.0,
            "max" : 349.0,
            "avg" : 308.11708860759495,
            "sum" : 97365.0
          }
        },
        {
          "key" : 350.0,
          "doc_count" : 12,
          "tweet_stats_bucket" : {
            "count" : 12,
            "min" : 352.0,
            "max" : 398.0,
            "avg" : 367.8333333333333,
            "sum" : 4414.0
          }
        },
        {
          "key" : 400.0,
          "doc_count" : 10,
          "tweet_stats_bucket" : {
            "count" : 10,
            "min" : 401.0,
            "max" : 444.0,
            "avg" : 421.6,
            "sum" : 4216.0
          }
        },
        {
          "key" : 450.0,
          "doc_count" : 1,
          "tweet_stats_bucket" : {
            "count" : 1,
            "min" : 490.0,
            "max" : 490.0,
            "avg" : 490.0,
            "sum" : 490.0
          }
        },
        {
          "key" : 500.0,
          "doc_count" : 2,
          "tweet_stats_bucket" : {
            "count" : 2,
            "min" : 525.0,
            "max" : 529.0,
            "avg" : 527.0,
            "sum" : 1054.0
          }
        },
        {
          "key" : 550.0,
          "doc_count" : 1,
          "tweet_stats_bucket" : {
            "count" : 1,
            "min" : 557.0,
            "max" : 557.0,
            "avg" : 557.0,
            "sum" : 557.0
          }
        },
        {
          "key" : 600.0,
          "doc_count" : 3,
          "tweet_stats_bucket" : {
            "count" : 3,
            "min" : 637.0,
            "max" : 646.0,
            "avg" : 641.6666666666666,
            "sum" : 1925.0
          }
        },
        {
          "key" : 650.0,
          "doc_count" : 3,
          "tweet_stats_bucket" : {
            "count" : 3,
            "min" : 687.0,
            "max" : 697.0,
            "avg" : 692.6666666666666,
            "sum" : 2078.0
          }
        },
        {
          "key" : 700.0,
          "doc_count" : 3,
          "tweet_stats_bucket" : {
            "count" : 3,
            "min" : 727.0,
            "max" : 736.0,
            "avg" : 731.0,
            "sum" : 2193.0
          }
        },
        {
          "key" : 750.0,
          "doc_count" : 1,
          "tweet_stats_bucket" : {
            "count" : 1,
            "min" : 776.0,
            "max" : 776.0,
            "avg" : 776.0,
            "sum" : 776.0
          }
        },
        {
          "key" : 800.0,
          "doc_count" : 0,
          "tweet_stats_bucket" : {
            "count" : 0,
            "min" : null,
            "max" : null,
            "avg" : null,
            "sum" : null
          }
        },
        {
          "key" : 850.0,
          "doc_count" : 2,
          "tweet_stats_bucket" : {
            "count" : 2,
            "min" : 850.0,
            "max" : 869.0,
            "avg" : 859.5,
            "sum" : 1719.0
          }
        }
      ]
    }
  }
}
```

> **Antwoord** 
```
GET tweets/_search
{
  "size": 0,
  "aggs": {
    "tweet_length_histogram": {
      "histogram": {
        "field": "tweet_length",
        "interval": 50
      },
      "aggs": {
        "bucket_stats": {
          "stats": {
            "field": "tweet_length"
          }
        }
      }
    }
  }
}
```
Bekijk tot slot volgende query en probeer te achterhalen wat deze precies doet:

```
GET tweets/_search
{
  "size": 0,
  "aggs": {
    "user_terms": {
      "terms": {
        "field": "username",
        "size": 10
      }
    }
  }
}
```

> **Antwoord**  
> Deze query groepeert de eerste 10 meest relevante tweets op username en toont ook het aantal tweets die in elastic overeenkomen met die username. 

## Mapping

Mapping in Elasticsearch is een belangrijk concept wanneer efficiëntie wordt nagestreefd. Het doel van mapping is om enerzijds de data op een correcte (compacte) manier op te slaan en anderzijds de query-time zo laag mogelijk te houden. Bij mapping zorgen we er voor dat onze data (deels) aan een bepaalde schema voldoet.

Het is niet de bedoeling om in deze workshop daar diep op in te gaan. Voor wie meer wil weten, kan de [mapping documentatie](https://www.elastic.co/guide/en/elasticsearch/reference/7.1/mapping.html) best eens doorlezen.

Toch willen we graag nog enkele pointers meegeven wat betreft mapping.

Het doel van volgende query is alle Tweets na 1 januari 2021 op te vragen. Voer uit:

```
GET tweets/_search
{
  "query": {
    "range": {
      "created_at": {
        "gt": "2021-01-01"
      }
    }
  }
}
```

Is dit een goed resultaat? 

> **Antwoord**  
> Nee

Bekijk de mapping van de Tweets index.

```
GET tweets/_mapping
```

Pas de bovestaande query aan zodanig er wel een correct resultaat is.

> **Antwoord**  
>
```
GET tweets/_search
{
  "query": {
    "range": {
      "@timestamp": {
        "gte": "2019-01-01"
      }
    }
  }
}
```

Merk op dat Elasticsearch bij het toevoegen van nieuwe *documents* zelf een mapping zal proberen aanmaken wanneer dit nodig blijkt te zijn. Het `created_at` field is daar een voorbeeld van. Echter werd voor jullie de mapping reeds ingesteld, bekijk maar eens de inhoud van `twitter_template.json`. Leg uit.

> **Antwoord**  
> `index_patterns`: de naam van de index  
> `number_of_shards`: hoeveel shards de index mag bevatten. Deze worden onderverdeeld per node  
> `number_of_replicas`: het aantal replica's er moeten worden gemaakt per primary shard op de andere nodes  
> `mappings.dynamic`: definieert hoe er wordt omgegaan in het geval dat een document nieuwe velden bevat.
> * true -> velden worden automatisch toegevoegd
> * false -> velden worden genegeeerd, ze worden niet geindexeerd om op te zoeken, maar ze zijn wel zichtbaar in `_source`
> * strict -> er zal een error optreden en het toevoegen zal geweigerd worden. nieuwe velden moeten manueel toegevoegd worden aan de mapping.



## Sharding

We bekijken even kort hoe Elasticsearch zijn data verdeelt: sharding. 

In de mapping template (`twitter_template.json`), staat ingesteld dat de Twitter index aangemaakt moet worden met 3 primary shards en 2 replicas. Jullie kregen tijdens de introductie wat uitleg hierover.

Voer de volgende queries uit, probeer het antwoord te begrijpen.

```
GET _cluster/health
GET _cat/indices
GET _cat/shards
```

Stop nu 1 van de Elasticsearch nodes (`docker-compose stop es03`). Voer bovenstaande queries opnieuw uit. Wat is het effect? Wat verwacht je dat er na verloop van tijd gaat gebeuren? 

> **Antwoord**  
> Het aantal nodes is gezakt naar **2**. Ook zijn er nu 3 shards `unassigned`.  
> De cluster health is `orange` doordat er een node is weggevallen.  
> Het aantal `active_shards` is wel hetzelfde gebleven.

Indien er nog een node zou uitgeschakeld worden, wat gaat er dan gebeuren? Denk goed na: dit is een strikvraag.

> **Antwoord**  
> De cluster health zal `rood` worden.  
> Het aantal `active_shards` zal halveren door het wegvalen van de 2de node.
Hierdoor zal er data verlies zijn.

## Data visualiseren

Navigeer naar *"Visualize"* in Kibana. In het vak *"Create index pattern"*  geef je als *"Index pattern" "tweets"* op, daarna klik je op *"Next step"*. Bij *"Time Filter field name"* selecteer je *"@timestamp"*, tot slot klik je op *"Create index pattern"*. 

Ga naar *"Visualize"*, klik op *"Create a visualization"*, daarna op *"Vertical bar"*. Selecteer *"tweets"*, bij *"Buckets"* klik op *"X-Axis"*, selecteer een *"Histogram"* aggregation, als *"Field"* kies je voor *"tweet_length"*, *"Minimum interval"* stel je in op *"50"*. Bij *"Custom Label"* kan je bv. *"Tweet Length"* opgeven. Bij de *"Y-Axis"* kan je eventueel ook nog het *"Custom Label"* aanpassen naar *"Tweets"*. Daarna kan je op de blauwe pijl drukken en zal de grafiek verschijnen. **Pas de time-range aan** (bovenaan rechts).

![](https://i.imgur.com/whbmJBI.png)

In sommige categorieën zullen maar een aantal Tweets zitten, je kan deze er eventueel uitfilteren, klik daarvoor bovenaan op *"Add filter"*,  als *"Field"* selecteer je *"tweet_length"*, als *"Operator"* kies je *"is between"* en dan bv. *"From"* 0 *"To"* 350.

![](https://i.imgur.com/YXgaO0j.png)

Bij de *"X-Axis"* zou je bijvoorbeeld nog *"sub-buckets"* kunnen toevoegen. Zie bijvoorbeeld zoals onderstaande screenshot.

![](https://i.imgur.com/MLReNOc.png)

Het eindresultaat zie je op onderstaande screenshot. De visualisatie groepeert de Tweets volgens hun berichtlengte. Binnen elke groep wordt weergegeven hoeveel Tweets er per type apparaat geïndexeerd zijn. 

![](https://i.imgur.com/IEZLu9H.png)

Sla de visualisatie op, kies een logische naam.

Maak nog een 2de visualisatie, een "Tag Cloud", doe zoals onderstaande screenshot:

![](https://i.imgur.com/fIZQ4mh.png)

### (Extra) Maak een dashobard

Probeer nu op basis van deze twee visualisaties een dashboard te bouwen.

## (Extra) Linux kennis aanscherpen?

Dit labo is intussen al een aantal keren gegeven. In de vorige versies werd er meer aandacht besteed aan het opzetten van de Elasticsearch cluster an sich. Dankzij feedback van studenten (ten zeerste geapprecieerd!) is dit bijgestuurd. Ik bespaar jullie bewust het opzetten van een Elasticsearch cluster die geschikt is in een productieomgeving. De focus ligt nu meer op het werken met de complete Elastic Stack en meer bepaald op het verwerken en analyseren van data. 

Het is echter misschien wel interessant om je Linux kennis een beetje aan te scherpen. Wie weet wordt jij in de toekomst wel een data engineer of kom je terecht in een "DevOps" team. Vaak zal je dan toch meer moeten kennen dan enkel en alleen het "werken met". Vandaar, voor wie zich geroepen voelt, hieronder een aantal interessante puntjes.

### Environment variables

In zowat alle operating systems (ja, ook in Windows) wordt er gebruik gemaakt van environment variables (vaak afgekort als env vars). Zoals je in dit labo hebt gezien, is dat ook het geval voor Docker containers.

In de vorige labo's zag je zeker al eens `$USER` staan. In dit labo heb je misschien `$HOME` gespot. Dit zijn twee voorbeelden van env vars die je standaard kan gebruiken onder Ubuntu. 

In `docker-compose.yml` file kunnen we gebruik maken van env vars met volgende syntaxis: `${VAR}`, zoals bv. `${IP}`. Die laatste is geen standaard env var, probeer bv. maar eens uit op je lokale VM's (labo RDBMS). 

We kunnen er zelf eenvoudig aanmaken, door bv. eentje toe te voegen aan `~/.bashrc` (onderaan). Bekijk het eens, onderaan staat:

```
export IP=`hostname -I | awk '{print $1}'`
```

Probeer bovenstaande te verklaren:
* Waarom export? 
* Wat doet awk?
* Wat is de betekenis van "|" (pipe)?

> **Antwoord**  
> * Export zorgt ervoor dat de variabelen ook beschikbaar zijn voor `child_processen`  
> * AWK zoekt een stuk tekst en doet daarmee een actie. In dit geval zoekt hij naar het eerste item die terug komt uit `hostname -I`.
> * "|" gebruikt de output van het eerste commande als input voor het tweede commando.  
> 

In de [environment variables](https://help.ubuntu.com/community/EnvironmentVariables) documentatie is meer info terug te vinden (al dan niet specifiek voor Ubuntu).

### IO scheduler

[Schedulers](https://en.wikipedia.org/wiki/Scheduling_(computing)) zijn een belangrijk principe in een operating system. Zo is er doorgaans een scheduler actief voor [I/O-operaties (input / output)](https://wiki.ubuntu.com/Kernel/Reference/IOSchedulers).

Simpel gezegd is het een soort van algoritme dat tracht lees- en schrijfacties logisch te orderen in een queue. Welke scheduler het beste is, hangt af van je opslagmedium en je use-case.

Bekijk eens [fstab](https://en.wikipedia.org/wiki/Fstab), `cat /etc/fstab`, file system table. In de options kolom zie je "defaults, noatime". Wat betekent die laatste optie en wat is het nut ervan? 

> **Antwoord**  
> Dit zorgt ervoor dat de `last_access_time` niet elke keer opnieuw wordt ingesteld bij een `file read`. Wat ervoor zorgt dat er niet elke keer een `write` operatie hoeft te gebeuren als een file wordt gelezen.  
> Dit kan voor een performance boost zorgen.


Nog een interessant weetje: sinds Ubuntu 14.04 is de default I/O scheduler gewijzigd naar deadline ten voordele van cfq. Wat zou de reden kunnen zijn?

> **Antwoord**
> Deadline gebruikt verschillende queues voor `read` en `write` en het is over het algemeen sneller dan cfq.

Een aantal ESXi hypervisors zorgen vandaag voor de virtuele machines. ESXi heeft zijn eigen filesystem (VMFS). Voer `cat /sys/block/sda/queue/scheduler` uit. Welke scheduler is voor jullie ingesteld, wat zou de reden kunnen zijn?

> **Antwoord**  
> noop, zij doen geen fancy werk om de I/O zo snel mogelijk te krijgen, maar werken gewoon FIFO. Aangezien de we dit in een VM runnen is er nog een onderliggende I/O schedulare van de host.

### Kernel settings

Vorige week hebben we het reeds kort gehad over de kernel. Dit is complexe matere. Zoals zowat alle software kunnen bepaalde instellingen van de Linux kernel aangepast worden. Met [sysctl](http://manpages.ubuntu.com/manpages/bionic/man8/sysctl.8.html) kunnen we instellingen "at runtime" aanpassen (m.a.w. tijdelijk). Om permantent wijzigen aan te brengen kan je het bestand `/etc/sysctl.conf` aanpassen.

Bekijk zeker eens het bestand, zijn wel wat interessante zaken in terug te vinden. Helemaal op het einde van het bestand is er voor jullie volgende lijn toegevoegd: `sysctl -w vm.max_map_count=262144`.

Deze lijn is zowel nodig voor Docker (toch van een bepaald punt) als Elasticsearch. Die laatste zal zelfs niet willen starten als de setting onvoldoende hoog is. Je kan er [hier](https://www.elastic.co/guide/en/elasticsearch/reference/7.6/vm-max-map-count.html) meer over lezen (en doorklikken op de referenties). 


### History

Tot slot nog een laatste (hopelijk) leuk weetje. Al je prachtige commando's worden opgeslagen in `~/.bash_history`. Met het `history` commando kan je  informatie ophalen. Dit wordt ook gebruikt wanneer je de toetsencombinatie `CTRL + R` / `CMD + R` gebruikt (je history doorzoeken).

De VM's die jullie vandaag hebben gebruikt hebben al deze best-practices reeds aan boord. Om jullie deze "netjes" te geven, is de histry gewist. Dat kan met volgende oneliner (typische term om meerdere commando's te combineren).

```
cat /dev/null > ~/.bash_history && history -c && exit
```

`/dev/null` is het zogenaamde [null device](https://en.wikipedia.org/wiki/Null_device). Eigenlijk zorgen we dat we "niets" schrijven naar `~/.bash_history`. Nog zo twee "speciallekes": `/dev/random` en `/dev/urandom` (probeer gerust eens uit, bv. met `cat`). Dit kan een "vreemd" resultaat geven, volgende [post](https://superuser.com/questions/637860/why-does-cat-dev-urandom-break-your-terminal) legt het verstaanbaar uit. 