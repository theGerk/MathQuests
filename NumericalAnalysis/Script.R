Newton = function(f, x, tolerance = .5 * 10 ^ -10, max.iterations = 100) {
    df = D(f) #the derivative
    x
    i = 0
    while (abs(f(x)) > tolerance && i < max.iterations) {
        x = x - f(x) / df(x)
        i = i + 1
    }
    return(v)
}

D = function(f, delta = 0.001) {
    d2 = function(x) {
        df = (f(x + delta) - f(x)) / delta
        return(df)
    }
    return(d2)
}