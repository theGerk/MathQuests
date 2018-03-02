library(ggplot2)



forward = function(f, delta = 0.001) {
    return( function(x) {
        return( (f(x + delta) - f(x)) / delta)
    })
}


backward = function(f, delta = 0.001) {
    return(function(x) {
        return((f(x) - f(x - delta)) / delta)
    })
}


threepoint = function(f, delta = 0.001) {
    return(function(x) {
        return((f(x + delta) - f(x - delta)) / 2 / delta)
    })
}

corrected = function(f, delta = 0.001) {
    return(function(x) {
        return((f(x + delta / 2) - f(x - delta / 2)) / delta)
    })
}



getall = function(f, x, expected, delta = 0.001) {
    ret = new.env()
    ret$forward = abs(forward(f, delta)(x) - expected)
    ret$backward = abs(backward(f, delta)(x) - expected)
    ret$threepoint = abs(threepoint(f, delta)(x) - expected)
    ret$corrected = abs(corrected(f, delta)(x) - expected)
    return(ret)
}


error = function(f, x, expected, delta = 0.001)
{
	errors = new.env()
	forward = abs(forward(f,delta)(x) - expected)
	backward = abs(backward(f, delta)(x) - expected)
	threepoint = abs(threepoint(f, delta)(x) - expected)
	corrected.threepoint = abs(corrected(f, delta)(x) - expected)
	return(as.data.frame(cbind(forward, backward, threepoint, corrected.threepoint)))
}

getplot = function(f, x, expected, delta = 0.001) {
	a = getall(f, x, expected, delta)
	plot(y = a$forward, x = delta, col = "red", ylab = "error", main = substitute(paste(x ^ 5 - 3 * x ^ 4 + 2.3 * x ^ 3 - 20 * x ^ 2 - x, ", at x = ", v), list(v = x)))
	points(y=a$corrected,x = delta, col = "green")
	points(y=a$threepoint, x = delta, col = "orange")
	points(y = a$backward, x = delta, col = "blue")
}

f = function(x) { return(x ^ 5 - 3 * x ^ 4 + 2.3 * x ^ 3 - 20 * x ^ 2 - x) }
fp = function(x) { return(5 * x ^ 4 - 12 * x ^ 3 + (2.3 * 3) * x ^ 2 - 40 * x - 1) }
s = seq(0, 1, length.out = 100000)


getplot(f, 0, fp(0), s)