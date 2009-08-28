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
namespace Db4objects.Db4o.Tutorial.F1.Chapter5
{
    /// <summary>
    /// Configuration used for StartServer and StopServer.
    /// </summary>
    public class ServerConfiguration
    {
        /// <summary>
        /// the host to be used.
        /// If you want to run the client server examples on two computers,
        /// enter the computer name of the one that you want to use as server. 
        /// </summary>
        public const string HOST = "localhost";  

        /// <summary>
        /// the database file to be used by the server.
        /// </summary>
        public const string FILE = "formula1.yap";

        /// <summary>
        /// the port to be used by the server.
        /// </summary>
        public const int PORT = 4488;

        /// <summary>
        /// the user name for access control.
        /// </summary>
        public const string USER = "db4o";
    
        /// <summary>
        /// the pasword for access control.
        /// </summary>
        public const string PASS = "db4o";
    }
}