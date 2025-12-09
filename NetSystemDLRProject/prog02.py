def binPower(x, n):
	if n == 0:
		return 1
	h = binPower(x, n // 2)
	h *= h
	if n % 2 == 1:
		h *= x
	return h

z = x + y
print("z =", z)