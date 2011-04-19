from tax import compute_tax

def ct(purchase):
	if purchase.Category == "CLOTHING" and purchase.Price > 50.00:
		purchase.Price = compute_tax(purchase.Price, .06)	
	elif purchase.Category == "CLOTHING":
		purchase.Price = purchase.Price
	elif purchase.Category == "SUBSCRIPTIONS":
		purchase.Price = compute_tax(purchase.Price, .03)
	else:
		purchase.Price = compute_tax(purchase.Price, .06)

def ma(purchase):
	
	if purchase.Category != "SUBSCRIPTIONS":
		purchase.Price = compute_tax(purchase.Price, .05)

def ny(purchase):
	
	#NYC waves tax for clothing items under $110
	if purchase.Category == "CLOTHING" and purchase.Price < 110.00 and purchase.City == "New York":
		purchase.Price = compute_tax(purchase.Price, .04375)
	elif purchase.City == "New York":
		purchase.Price = compute_tax(purchase.Price, .08375)
	else:
		purchase.Price = compute_tax(purchase.Price, .04)
		


