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

### Power

Direction: Computer -> Track, Track -> Computer

Topic: `<prefix>/power`

Data:
```
{
    "active": 0 | 1
}
```

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

### Binary Outputs 

Direction: Computer -> Track

Topic: `<prefix>/binary-output`

Data:
```
{
    "address": "module/number",
    "value": 0 | 1
}
```

### Switches

Direction: Computer -> Track

Topic: `<prefix>/switch`

Data:
```
{
    "address": "module/number",
    "direction": "straight|off"
}
```

### Switch feedbacks

Direction: Track -> Computer 

Topic: `<prefix>/switch-feedback`

Data:
```
{
    "address": "module/number",
    "direction": "straight|off"
}
```

### Clocks (4-stage)

Direction: Computer -> Track

Topic: `<prefix>/clock-4-stage`

Data:
```
{
    "period": "morning|afternoon|evening|night"
}
```
