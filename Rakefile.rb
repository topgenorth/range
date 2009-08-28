COMPILE_TARGET = "debug"
require "BuildUtils.rb"

include FileTest

require 'rubygems'

require 'zip/zip'
require 'zip/zipfilesystem'

RESULTS_DIR = "results"
BUILD_NUMBER = "0.3.0."
PRODUCT = "RangeLog"
COPYRIGHT = 'Copyright 2009 Tom Opgenorth. All rights reserved.';
COMMON_ASSEMBLY_INFO = 'src/CommonAssemblyInfo.cs';
CLR_VERSION = "v3.5"

versionNumber = ENV["BUILD_NUMBER"].nil? ? 0 : ENV["BUILD_NUMBER"]

msbuild = "xbuild"

task :default => :build

task :build => [:clean, :compile]

task :clean do
	sh "xbuild /t:Clean RangeLog.sln"
	sh "rm -rf src/Opgenorth.RangeLog.UI.WinForms/bin/*"
	sh "rm -rf src/Opgenorth.RangeLog.Core/bin/*"
	sh "rm -rf src/Opgenorth.RangeLog.Tests/bin/*"

	sh "rm -rf src/Opgenorth.RangeLog.UI.WinForms/obj/*"
	sh "rm -rf src/Opgenorth.RangeLog.Core/obj/*"
	sh "rm -rf src/Opgenorth.RangeLog.Tests/obj/*"
end

desc "Compiles the app"
task :compile => [:clean, :version] do
	XBuildRunner.compile :compilemode => COMPILE_TARGET, :solutionfile => 'RangeLog.sln'
end

desc "Update the version information for the build"
task :version do
	builder = AsmInfoBuilder.new(BUILD_NUMBER, {'Product' => PRODUCT, 'Copyright' => COPYRIGHT})
	buildNumber = builder.buildnumber
	puts "The build number is #{buildNumber}"
	builder.write COMMON_ASSEMBLY_INFO
end

desc "Runs unit tests"
task :test => [:unit_test]

desc "Runs unit tests"
	task :unit_test => :compile do
	sh 'mono tools/NUnit/bin/nunit-console-x86.exe src/Opgenorth.RangeLog.Tests/bin/Debug/Opgenorth.RangeLog.Tests.dll'
	sh 'mono tools/NUnit/bin/nunit-console-x86.exe src/Opgenorth.RangeLog.Tests/bin/Debug/Opgenorth.RangeLog.Tests.dll'

#	runner = NUnitRunner.new :compilemode => COMPILE_TARGET, :source => 'src', :platform => 'x86'
#	runner.executeTests ['Opgenorth.RangeLog.Tests']
end

