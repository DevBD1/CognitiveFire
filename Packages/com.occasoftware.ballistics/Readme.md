# README

## Contents

- About
- Installation Instructions
- Usage Instructions
- Requirements
- Support

## About

Ballistics is a physics-based bullet ballistics system for Unity.
You can predict bullet trajectories, or run realtime trajectory simulations.

## Installation Instructions

1. Import Ballistics into your project.

## Usage Instructions

1. Create a script that inherits from the BallisticsProjectile class.
2. Call the Simulate() method

I strongly recommend that you review the docs.
<https://docs.occasoftware.com/ballistics/>

## Quick Start

<https://docs.occasoftware.com/ballistics/quick-start>

## Public API

<https://docs.occasoftware.com/ballistics/ballistics-api>

## Troubleshooting

- Ensure that you have created and assigned a Projectile, Environment, and SimulationConfig and verify the configuration for each.
- Ensure that your realtime projectiles inherit from BallisticsProjectile.
- Ensure that you are subscribing and unsubscribing to the built-in callbacks.

## Additional Notes

The demo scene for this asset was made in Unity URP and may not work on all versions of Unity. The Editor and Runtime asset package should work on all up-to-date versions from 2021.3 LTS forward.

## Support

If you need any help at all, join us on Discord:
<https://www.occasoftware.com/discord>
