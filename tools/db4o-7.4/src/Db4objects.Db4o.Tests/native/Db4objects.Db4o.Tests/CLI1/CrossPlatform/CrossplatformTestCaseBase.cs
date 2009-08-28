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
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using Db4objects.Db4o.Config;
using Db4objects.Db4o.Ext;
using Db4objects.Db4o.Messaging;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Tests.Util;
using Db4oUnit;

namespace Db4objects.Db4o.Tests.CLI1.CrossPlatform
{
	internal abstract class CrossplatformTestCaseBase : ITestLifeCycle
	{
#if !CF
		protected JavaSnippet JavaClientQuery()
		{
			return new JavaSnippet(
				"com.db4o.crossplatform.test.client.ClientCrossPlatform",
				@"package com.db4o.crossplatform.test.client;

import java.util.Date;
import com.db4o.*;
import com.db4o.query.Query;
import com.db4o.config.Configuration;

public class ClientCrossPlatform {
	public static void main(String[] args) throws java.text.ParseException { 
		if (args.length < 4) {
			System.err.println(""Java client query: invalid # of arguments. Expected: 5 (port user passwd [ [qp | qm name] | [year name local-release-date]]) got "" + args.length);
			return;
		}
		
		ObjectContainer db = Db4o.openClient(config(), ""localhost"", Integer.parseInt(args[0]), args[1], args[2]);
		
		if (args[3].equals(""query-movie"")) {
			System.out.print(queryMovies(db));	
		} 
        else if (args[3].equals(""query-person"")) {
			System.out.print(queryPersons(db, args[4]));	
		} 
        else {	
			insertPerson(
					db, 
					args[4], 
					Integer.parseInt(args[3]),
					//FIXME
					//java.text.DateFormat.getInstance().parse(args[5])
					null);	
		}
			
		db.close();
	}

	private static String queryMovies(ObjectContainer db) {
		StringBuffer output = new StringBuffer();
		Movies movies = getMovies(db);
		for (int i = 0; i < movies.length(); i++) {
			output.append(movies.get(i,0) + ""/"" + movies.get(i,1) + ""\r\n"");
		}
		return output.toString();
	}

	private static Movies getMovies(ObjectContainer db) {
		Query q = db.query();
		q.constrain(Movies.class);
	
		ObjectSet results = q.execute();
		return (Movies) results.next();
    }

	private static String queryPersons(ObjectContainer db, String tbq) {
		StringBuffer output = new StringBuffer();
		ObjectSet results = getPersons(db, tbq);
		while (results.hasNext()) {
			Person person = (Person) results.next();
			output.append(person + ""\n"");
		}
		
		return output.toString();
	}

	private static void insertPerson(ObjectContainer db, String name, int year, Date localReleaseDate) {
		db.set(new Person(name, year, localReleaseDate));
	}

	private static Configuration config() {
		Configuration config = Db4o.newConfiguration();" + 
		GetClientAliases() +
@"		return config;
	}

	private static ObjectSet getPersons(ObjectContainer db, String name) {
		Query query = db.query();
	
		query.constrain(Person.class);
		query.descend(""_name"").constrain(name).like();
		
		return query.execute();
	}

	public static class Person {
		public String _name;
		public int _year;
		public Date _localReleaseDate;

		public Person(String name, int year, Date localReleaseDate) {
			_name = name;
			_year = year;
			_localReleaseDate = localReleaseDate;
		}

		public Person() {
		}

		public String toString() {
			return _name + ""|"" + _year;
		}		
	}

	public static class Movies {
		private String[][] _notes;
		private int _counter;

		public int length() {
			return _notes.length;
		}

		public String get(int movieIndex, int infoIndex) {
			if (movieIndex >= _counter) {
				return ""index to big!"";
			}

			return _notes[movieIndex][infoIndex];
		}
    }
}");
		}

		protected string RunJavaQuery(string type, string tbf)
		{
			JavaSnippet javaClient = JavaClientQuery();
			CompileJava(javaClient);

			string queryResult = JavaServices.java(javaClient.MainClassName, HOST_PORT.ToString(), USER_NAME, USER_PWD, type, tbf);
			Assert.IsGreater(0, queryResult.Length);

			return queryResult;
		}

		protected void CompileJava(JavaSnippet snippet)
		{
			if (!AlreadyCompiled(snippet.MainClassName))
			{
				FlagAsCompiled(snippet.MainClassName);
				JavaServices.CompileJavaCode(snippet.MainClassFile, snippet.SourceCode);
			}
		}

		private void FlagAsCompiled(string mainClassName)
		{
			_compiled[mainClassName] = true;
		}

		private bool AlreadyCompiled(string mainClassName)
		{
			return _compiled.ContainsKey(mainClassName) ? _compiled[mainClassName] : false;
		}

		protected static IEnumerator ParseJavaClientResults(string result)
		{
			IList<Person> personList = new List<Person>();

			TextReader reader = new StringReader(result);
			string line ;
			while ((line = reader.ReadLine())  != null)
			{
				Match match = Regex.Match(line, @"(.+)\|(.+)\Z");
				personList.Add(NewPersonFromMatch(match));
			}

			return personList.GetEnumerator();
		}

		private static Person NewPersonFromMatch(Match match)
		{
			return new Person(
				match.Groups[1].Captures[0].Value,
				Int32.Parse(match.Groups[2].Captures[0].Value),
				default(DateTime));
						//DateTime.Parse(match.Groups[3].Captures[0].Value));
		}

		private void Reconnect()
		{
			if (_client != null)
			{
				_client.Close();
				_client = null;
			}

			Connect();
		}

		private void Connect()
		{
			while (true)
			{
				_client = null;
				try
				{
					_client = Db4oFactory.OpenClient(Config(), "localhost", HOST_PORT, USER_NAME, USER_PWD);
					break;
				}
				catch (SocketException se)
				{
				}
				catch(DatabaseClosedException dce)
				{
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
		}

		protected void ShutdownServer()
		{
			if (_client != null)
			{
				IMessageSender messageSender = _client.Ext().Configure().ClientServer().GetMessageSender();
				messageSender.Send(new StopServer());
			}
		}

		public void SetUp()
		{
			if (!JavaServices.CanRunJavaCompatibilityTests())
			{
				throw new TestException("Fail to run cross platform tests", null);
			}

#if !RUNNING_OUTSIDE_SERVER
			StartServer();
#endif

			Connect();
		}

		public void TearDown()
		{
#if !RUNNING_OUTSIDE_SERVER
			ShutdownServer();
#endif
		}

		protected static bool FindAllJoes(Person candidate)
		{
			return candidate.Name == JOE_NAME;
		}

		protected void AssertInsertFromJavaClient(string name, int year, DateTime localReleaseDate)
		{
			InsertFromJavaClient(year, name, localReleaseDate);

			AssertExactlyOneInstance(name, year, localReleaseDate);
		}

		protected void InsertFromJavaClient(int year, string name, DateTime localReleaseDate)
		{
			JavaSnippet javaClient = JavaClientQuery();
			CompileJava(javaClient);

			string insertResult = JavaServices.java(javaClient.MainClassName, HOST_PORT.ToString(), USER_NAME, USER_PWD, year.ToString(), name, localReleaseDate.ToShortDateString());
			Assert.AreEqual("", insertResult);
		}

		private void AssertExactlyOneInstance(string name, int year, DateTime localReleaseDate)
		{
			Iterator4Assert.AreEqual(
				new Person[] { new Person(name, year, localReleaseDate) },
				SodaQueryByName(name).GetEnumerator());
		}

		protected void AssertQueryFromJavaClient()
		{
			Iterator4Assert.SameContent(
				Array.FindAll(
					persons,
					delegate(Person candidate) { return candidate.Name.StartsWith(WOODY_NAME); }),
				ParseJavaClientResults(RunJavaQuery("query-person", WOODY_NAME)));
		}

		protected ICollection SodaQueryByName(string name)
		{
			IQuery query = _client.Query();
			query.Constrain(typeof(Person));
			query.Descend("_name").Constrain(name);

			return query.Execute();
		}

	    protected delegate void TestAction();
		protected void ReconnectAndRun(TestAction assertion)
		{
			Reconnect();
			assertion();
		}

		protected abstract string GetClientAliases();
		protected abstract void StartServer();
		protected abstract IConfiguration Config();

		protected const int HOST_PORT = 3739;
		protected const string USER_NAME = "db4o_cpt";
		protected const string USER_PWD = "test";
		protected const string JOE_NAME = "Joe";
		protected const string WOODY_NAME = "Woody";

		protected static readonly Person[] persons = new Person[]
		                                           	{
		                                           		new Person("Viktor Navorski", 2004, new DateTime(2004, 09, 10)),
		                                           		new Person(JOE_NAME, 1, new DateTime(1971, 05, 01)),
		                                           		new Person("Carl Hanratty", 2002, new DateTime(2003, 02, 21)),
		                                           		new Person(WOODY_NAME, 1995, new DateTime(1995, 12, 22)),
		                                           		new Person("Woody Car", 2006, new DateTime(2006, 06, 30)),
		                                           		new Person(JOE_NAME, 2, new DateTime(2004, 02, 23)),
		                                           	};

		protected IObjectContainer _client;
		private IDictionary<string, bool> _compiled = new Dictionary<string, bool>();

#else
		public void SetUp()
		{
		}

		public void TearDown()
		{
		}
#endif
	}

	internal class StopServer
	{
		public int _id;
	}

	public class UnoptimizideJoeFinder : Predicate
	{
		public bool Match(Person candidate)
		{
			return candidate.Name.ToUpper() == "Joe";
		}
	}
}
