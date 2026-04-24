using Six.Demon.Bag.Trees;

Console.WriteLine("Hello, World!");

var node = new Tree<string>(".") {
	new("1"){
		new("1.1"),
		new("1.2")
	},
	new("2"){
		new("2.1"),
		new("2.2")
	},
	new("3"){
		new("3.1"),
		new("3.2")
	}
};