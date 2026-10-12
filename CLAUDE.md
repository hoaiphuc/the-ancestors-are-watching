# The ancestor are watching

2D incremental game, Unity 6 (Universal 2D / URP), built for a portfolio. Reviewers will read the code.

## Working style
- Build step by step, one feature at a time. Show a short plan for each step and wait for approval before coding.
- The user does editor work themselves, so automate it via `Tools > Setup Scene` and list anything left to do by hand.

## Game design

Story: the player is a cultivator whose realm is too high to descend to the mortal world. Their kin is gravely injured, and the player can only protect him with their power from afar.

Core loop:
- Kin sits in the centre of the screen. He cannot act. He has a single Health bar that doubles as his recovery: he starts each run at 1 HP and regenerates slowly.
- Enemies spawn at the screen edges and walk toward him. On reaching him, they damage his Health.
- The player clicks enemies to strike them from the heavens. Killed enemies drop Spirit Stones (currency).
- A run ends when his Health reaches 0. The player keeps Spirit Stones, spends them on permanent upgrades, then starts a new run.
- If his Health regenerates to full during a single run, he breaks through to the next cultivation realm (permanent). Health resets to 1 every run.
- Realms: Qi Condensation -> Foundation Establishment -> Golden Core -> Nascent Soul -> Ascension. Each realm makes enemies stronger and more numerous. Reaching Ascension ends the game with a victory screen.

Later progression (not Phase 1):
- Click techniques grow: Finger Strike -> Chain Lightning (jumps between enemies) -> Heavenly Palm (large area, cooldown). Upgrades add auto-strikes.
- Helpers bought with Spirit Stones: Spirit Beast (fights near him), Flying Swords (orbit him and hit enemies automatically), Talismans/Formations (area slow, burn, or shield).

## Technical rules
- C#, Unity 6, 2D URP. Use the Input System package for mouse input.
- Data-driven: enemies, helpers, and upgrades are ScriptableObjects, so new content needs no new code.
- Object pooling (UnityEngine.Pool) for enemies, projectiles, and damage numbers.
- Keep systems decoupled: C# events for enemy death, currency changes, and run end.
- Save permanent progress (Spirit Stones, upgrades, current realm) as JSON in Application.persistentDataPath.
- Use doubles for currency and damage. Format as 1.2K, 3.4M, etc.
- Placeholder art (simple coloured shapes) so the game is playable early. Keep art easy to swap.
- Editor menu item `Tools > Setup Scene` builds the scene, prefabs, and ScriptableObject assets automatically.
- Clean, readable code with short comments explaining why, not what.

## Phase 1 (build only this; do not start Phase 2 until asked)
1. Kin in the centre with a Health bar (starts at 1 HP, regenerates).
2. One enemy type spawning from random screen edges, spawn rate increasing during the run.
3. Click to damage enemies, with a hit flash and floating damage numbers.
4. Spirit Stones on kill, shown in the HUD.
5. Run end screen -> upgrade screen with 5 upgrades (click damage, kin max Health, health regen speed, Spirit Stone gain, auto-strike) -> new run.
6. Save and load.

When Phase 1 is done: explain how to set it up and test it, and suggest what Phase 2 should include.

## Progress
- [ ] 1. Kin with Health bar (built, awaiting user test)
- [ ] 2. Enemy spawning
- [ ] 3. Click damage, hit flash, damage numbers
- [ ] 4. Spirit Stones and HUD
- [ ] 5. Run end and upgrade screens
- [ ] 6. Save and load
