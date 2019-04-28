﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace R2Script.Parse.AST
{
	public class Expr_Binary : Expression
	{
		public Expression Left, Right;
		public string Operator;

		public Expr_Binary(int line) : base(line)
		{
		}

		public Expression TryContract()
		{
			if (Left is Expr_Binary bl)
				Left = bl.TryContract();
			if (Right is Expr_Binary br)
				Right = br.TryContract();
			if (!(Left is Expr_Value && Right is Expr_Value))
				return this;
			switch (Operator)
			{
				case "+":
					return new Expr_Value(Line)
					{
						Value = (Convert.ToInt64((Left as Expr_Value).Value) +
						Convert.ToInt64((Right as Expr_Value).Value)).ToString()
					};
				case "-":
					return new Expr_Value(Line)
					{
						Value = (Convert.ToInt64((Left as Expr_Value).Value) -
						Convert.ToInt64((Right as Expr_Value).Value)).ToString()
					};
				case "*":
					return new Expr_Value(Line)
					{
						Value = (Convert.ToInt64((Left as Expr_Value).Value) *
						Convert.ToInt64((Right as Expr_Value).Value)).ToString()
					};
				default:
					return this;
			}
		}
	}
}