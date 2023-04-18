﻿using System.Numerics;

namespace RayTracer;

using Shapes;

public class Program
{
    public static void Main(string[] args)
    {
        var aspectRatio = 16.0f / 9.0f;
        var width       = 1920f;
        var height      = (int)(width / aspectRatio);

        var colorBuffer = new ColorBuffer(width, height);

        var viewportHeight = 2.0f;
        var viewportWidth  = aspectRatio * viewportHeight;
        var focalLength    = 5.0f;
        var origin         = new Vector3(0, 0, 7.0f);
        var lookDirection  = new Vector3(0, 0, -1.0f);

        var camera = new Camera(viewportHeight,
                                viewportWidth,
                                focalLength,
                                origin,
                                lookDirection);

        var world = new ShapeCollection(new()
                                        {
                                            new Sphere(name:     "Transparent",
                                                       material: new()
                                                                 {
                                                                     MaterialColor        = new Vector3(1.0f, 1.0f, 1.0f),
                                                                     Smoothness           = 1.0f,
                                                                     IndexOfRefraction    = 1.4f,
                                                                     ReflectiveConstant   = 0.0f
                                                                 },
                                                       center:   new(1.0f, -0.1f, 1.0f),
                                                       radius:   0.3f),
                                            new Sphere(name:     "Mirror",
                                                       material: new()
                                                                 {
                                                                     MaterialColor        = new Vector3(1.0f, 1.0f, 1.0f),
                                                                     Smoothness           = 1.0f,
                                                                     ReflectiveConstant   = 1.0f
                                                                 },
                                                       center:   new(-0.5f, 0.0f, -2.5f),
                                                       radius:   0.5f),
                                            new Sphere(name:     "Center-Right",
                                                       material: new()
                                                                 {
                                                                     MaterialColor        = new Vector3(1.0f, 0.1f, 0.1f),
                                                                     Smoothness           = 0.0f,
                                                                     ReflectiveConstant   = 1.0f
                                                                 },
                                                       center:   new(1.1f, 0, -1.5f),
                                                       radius:   0.5f),
                                            new Sphere(name:     "Right",
                                                       material: new()
                                                                 {
                                                                     MaterialColor       = new Vector3(0.1f, 0.1f, 1.0f),
                                                                     Smoothness          = 0.0f,
                                                                     ReflectiveConstant  = 1.0f
                                                                 },
                                                       center:   new(2.2f, -0.02f, -1.5f),
                                                       radius:   0.5f),
                                            new Sphere(name:     "Ground",
                                                       material: new()
                                                                 {
                                                                     MaterialColor      = new Vector3(0.95f, 0.95f, 0.95f),
                                                                     ReflectiveConstant = 1.0f
                                                                 },
                                                       center:   new(0, -100.5f, -1.0f),
                                                       radius:   100.0f),
                                            new Sphere(name:     "Back-Light",
                                                       material: new()
                                                                 {
                                                                     EmissionColor    = Vector3.One,
                                                                     EmissionStrength = 1.0f
                                                                 },
                                                       center:   new(-5.0f, 10.0f, 10.0f),
                                                       radius:   10.0f)
                                        });

        var maxDepth = 5;

        var samplesPerPixel = 1_000;

        var rayTracer = new RayTracer(camera,
                                      colorBuffer,
                                      world,
                                      maxDepth,
                                      samplesPerPixel,
                                      Environment.ProcessorCount);

        rayTracer.Run();
    }
}