def filter_small(item):
	item.Bar = item.Bar.lower()

def filter_medium(item):
	item.Bar = item.Bar.title()

def filter_large(item):
	item.Bar = item.Bar.upper()
