# MQTT Command station 

The MQTT command station communicates with elements on the track over the MQTT protocol.

## Messages 

### Locs 

Direction: Computer -> Track

Topic: `<prefix>/loc`

Data: 
```
{
    "address": "module/number",
    "speed": speedInSteps,
    "direction: "forward|reverse"
}
```

TODO Functions 

### Binary Sensors 

Direction: Track -> Computer 

Topic: `<prefix>/binary-sensor`

Data:
```
{
    "address": "module/number",
    "value": 0 | 1
}
```

