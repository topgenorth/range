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
using System.Diagnostics;
using Db4oTool.Core;
using Db4oTool.NQ;
using Db4oTool.TA;

namespace Db4oTool
{
	public class Program
	{
		public static int Main(string[] args)
		{
			Trace.Listeners.Add(new TextWriterTraceListener(Console.Error));

			ProgramOptions options = new ProgramOptions();
			try
			{	
				options.ProcessArgs(args);
				if (!options.IsValid)
				{
					options.DoHelp();
					return -1;
				}

				Run(options);
			}
			catch (Exception x)
			{
				ReportError(options, x);
				return -2;
			}
			return 0;
		}

		public static void Run(ProgramOptions options)
		{
            foreach (string fileName in options.StatisticsFileNames)
            {
                new Statistics().Run(fileName);
            }
            if (options.Assembly == null)
            {
                return;
            }

			using (new CurrentDirectoryAssemblyResolver())
			{
				RunPipeline(options);
			}
		}

		private static void RunPipeline(ProgramOptions options)
		{
			InstrumentationPipeline pipeline = new InstrumentationPipeline(GetConfiguration(options));
			if (options.NQ)
			{
				pipeline.Add(new DelegateOptimizer());
				pipeline.Add(new PredicateOptimizer());
			}
			if (options.TransparentPersistence)
			{
				pipeline.Add(new TAInstrumentation());
			}
			foreach (IAssemblyInstrumentation instr in Factory.Instantiate<IAssemblyInstrumentation>(options.CustomInstrumentations))
			{
				pipeline.Add(instr);
			}
			if (!options.Fake)
			{
				pipeline.Add(new SaveAssemblyInstrumentation());
				if (pipeline.Context.IsAssemblySigned())
				{
					pipeline.Context.TraceWarning("Warning: Assembly {0} has been signed; once instrumented it will fail strong name validation (you will need to sign it again).", pipeline.Context.Assembly.Name.Name);
				}
			}
			pipeline.Run();
		}

		private static void ReportError(ProgramOptions options, Exception x)
		{
			if (options.Verbose)
			{
				Console.WriteLine(x);
			}
			else
			{
				Console.WriteLine(x.Message);
			}
		}

		private static Configuration GetConfiguration(ProgramOptions options)
		{
			Configuration configuration = new Configuration(options.Assembly);
			configuration.CaseSensitive = options.CaseSensitive;
			configuration.PreserveDebugInfo = options.Debug;
			if (options.Verbose)
			{
				configuration.TraceSwitch.Level = options.PrettyVerbose ? TraceLevel.Verbose : TraceLevel.Info;
			}
            foreach (TypeFilterFactory factory in options.Filters)
            {
                configuration.AddFilter(factory());
            }
			return configuration;
		}
	}
}