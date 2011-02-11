rule_for "Product":
	title def(x):
		return "${x.Manufacturer.Name.ToUpper()} - ${x.Name}"
	description def(x):
		return x.Description
	link def(x):
		return "http://codevoyeur.com/products/${x.Id}"
	pubDate def(x):
		return x.CreateDate.ToString("s")		

rule_for "ProductReview":
	title {x | return "${x.Product.Manufacturer.Name.ToUpper()} - ${x.Product.Name} Review by ${x.UserName}" }
	description { x | x.Review }
	link {x | "http://codevoyeur.com/productreviews/${x.Id}" }
	pubDate { x | x.ReviewDate.ToString("s") }
	