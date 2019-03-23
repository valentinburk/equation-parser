# Equations parser

Provides basic functionality for conversion provided equations to canonical form.

### Example

```
x^2 + 3.5xy + y = y^2 - xy + y
will be transformed into
x^2 - y^2 + 4.5xy = 0
```

### Features

1. **Console mode**
Provide equations simply typing in your Console

2. **File mode**
Provide path to the file with the list of equations separated with linebreaks and you'll recieve the file with the list of transformed equations

### Updates

Version 2.0 of the **Equations parser** provides several improvements:
1. You can use the floating point powers: `x^2.5`, and negative powers: `x^-2.5`
2. It is allowed to have variables in different order in different places of equation: `xy=yx => 0=0`
3. Improved stability and preformance

### Plans for future versions

In future versions:
1. Web downloader for equation
2. Possibility to work with parentheses
3. Additional features
