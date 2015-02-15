Advanced-NMR-Methods-and-Metal-Based-Catalysts
==============================================
Here from the research group would like to let you some applications absolutly free that give you the opportunity to manipulate spectrums by new tools  where all are developed by us at Universtity of Almería. This tools are programmed in Visual C# and user the Helixtoolkit as dll.
NMRMBC Viewer
The first tool is called NRMMBC Viewer, this tool let you read spectrums form bruker's spectrometer and allow save the spectrums in two differnts formats. For avanced users the first format is in XML and with some methods is easy to read, so the second the format is a ticpicaly format txt where there are two columns which represent PPM and Intesity, also this kind of file is oriented in other to be used with orders programs like MATLAB or R. Therefore you can read in an easy way and work with MATLAB for instance.
This tool use a NMRMBC library and its usage is as follow:
1.- That use the class Coordenadas (Coodernates) in order to get the ppm and intensity. So you save a coordernadas for each spectrum.
2.- The class new_spectrum is used to read the directories wher the set of spectrum are located. Remember that the tree of the spectrum folders are for instance:
                            - 120jm56cg
                            ------------10
                            --------------pdata
                            -------------------1
And must be like this. Also, the class new_spectrum give back three arguments: set of experiments, set of directories and filepath where the esperiment's features is located.
3.- The class read is used to read de parameters of the experiment and the binary file where the espectrum is saved in its original form.

Example of software:

NMRMBC.new_spectrum ns = new NMRMBC.new_spectrum();
NMRMBC.read rd = new NMRMBC.read();
NMRMBC.diffusion diffusion = new NMRMBC.diffusion();
NMRMBC.espectrum espectro = new NMRMBC.espectrum();
NMRMBC.diffusion difusion = new NMRMBC.diffusion();

experimentos = ns.experimentos;
directorios = ns.directorio;
filepath = ns.filepath;

rd.read_param(filepath + "\\" + experimentos[i] + "\\" + "pdata\\1\\procs");
rd.readReal(filepath, experimentos[i]);
espectro = rd.ESPECTRUM;
if (diffusion.add(experimentos[i], espectro)) // if is true then I can work out with the spectrum
{
}

Don't matter I let the solution here in order to read  the spectrum.
