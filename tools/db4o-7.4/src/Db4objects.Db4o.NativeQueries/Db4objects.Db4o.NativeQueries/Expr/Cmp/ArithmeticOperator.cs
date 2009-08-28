/* Copyright (C) 2004 - 2008  db4objects Inc.  http://www.db4o.com

This file is part of the db4o open source object database.

db4o is free software; you can redistribute it and/or modify it under
the terms of version 2 of the GNU General Public License as published
by the Free Software Foundation and as clarified by db4objects' GPL 
interpretation policy, available at
http://www.db4o.com/about/company/legalpolicies/gplinterpretation/
Alternatively you can write to db4objects, Inc., 1900 S Norfolk Street,
Suite 350, San Mateo, CA 94403, USA.

db4o is distributed in the hope that it will be useful, but WITHOUT ANY
WARRANTY; without even the implied warranty of MERCHANTABILITY or
FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License
for more details.

You should have received a copy of the GNU General Public License along
with this program; if not, write to the Free Software Foundation, Inc.,
59 Temple Place - Suite 330, Boston, MA  02111-1307, USA. */
namespace Db4objects.Db4o.NativeQueries.Expr.Cmp
{
	public sealed class ArithmeticOperator
	{
		public const int AddId = 0;

		public const int SubtractId = 1;

		public const int MultiplyId = 2;

		public const int DivideId = 3;

		public static readonly Db4objects.Db4o.NativeQueries.Expr.Cmp.ArithmeticOperator 
			Add = new Db4objects.Db4o.NativeQueries.Expr.Cmp.ArithmeticOperator(AddId, "+");

		public static readonly Db4objects.Db4o.NativeQueries.Expr.Cmp.ArithmeticOperator 
			Subtract = new Db4objects.Db4o.NativeQueries.Expr.Cmp.ArithmeticOperator(SubtractId
			, "-");

		public static readonly Db4objects.Db4o.NativeQueries.Expr.Cmp.ArithmeticOperator 
			Multiply = new Db4objects.Db4o.NativeQueries.Expr.Cmp.ArithmeticOperator(MultiplyId
			, "*");

		public static readonly Db4objects.Db4o.NativeQueries.Expr.Cmp.ArithmeticOperator 
			Divide = new Db4objects.Db4o.NativeQueries.Expr.Cmp.ArithmeticOperator(DivideId, 
			"/");

		private string _op;

		private int _id;

		private ArithmeticOperator(int id, string op)
		{
			_id = id;
			_op = op;
		}

		public int Id()
		{
			return _id;
		}

		public override string ToString()
		{
			return _op;
		}
	}
}
