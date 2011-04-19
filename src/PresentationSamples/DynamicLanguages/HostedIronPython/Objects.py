from HostedIronPython import *
from HostedIronPython.Model import *

lister = AlbumLister()
finder = AlbumFinder("Albums.xml")

lister.Finder = finder

instances["AlbumLister"] = lister
instances["AlbumFinder"] = finder